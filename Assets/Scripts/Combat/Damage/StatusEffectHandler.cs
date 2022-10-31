using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using TeamOne.EvolvedSurvivor;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    [SerializeField] protected DamageHandler damageHandler;
    [SerializeField] protected CharacterMovement movement;
    [SerializeField] protected TopDownController2D controller;
    [SerializeField] protected AIBrain aiBrain;

    void OnEnable()
    {
        damageHandler.ResetEffects();
        movement.ContextSpeedStack.Clear();
    }

    void OnDisable()
    {
        damageHandler.ResetEffects();
        movement.ContextSpeedStack.Clear();
    }

    // Plasma
    public void ApplyChaining(Damage damage, GameObject target)
    {
        target.GetComponent<DamageReceiver>().TakeDamage(damage);
    }

    // Cryo
    public void FreezeForDuration(float duration)
    {
        damageHandler.DisableOutgoingDamageForDuration(duration);
        movement.SetContextSpeedMultiplier(0);
        aiBrain.BrainActive = false;
        StartCoroutine(ResetMovementSpeedAfterDuration(duration));
        StartCoroutine(EnableBrainAfterDuration(duration));
    }
    // Force
    public void ApplyForce(Vector3 direction, float magnitude)
    {
        controller.Impact(direction, magnitude);
    }

    // Infect
    public void SlowForDuration(float magnitude, float duration)
    {
        movement.SetContextSpeedMultiplier(magnitude);
        StartCoroutine(ResetMovementSpeedAfterDuration(duration));
    }

    // Pyro
    public void DamageOverTimeForDuration(Damage damage, float tickRate, float duration)
    {
        damageHandler.ApplyDamageOverTime(damage, tickRate, duration);
    }

    IEnumerator ResetMovementSpeedAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        movement.ResetContextSpeedMultiplier();
    }

    IEnumerator EnableBrainAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        aiBrain.BrainActive = true;
    }
}
