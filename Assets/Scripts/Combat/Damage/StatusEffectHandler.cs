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

    // Freeze
    private float freezeTimer;
    // Slow
    private float slowTimer;
    // DamageOverTime
    private float damageOverTimeTimer;
    private float tickRate;
    private Damage damage;
    private float lastTickTime;

    private void OnEnable()
    {
        damageHandler.EnableOutgoingDamage();
        aiBrain.BrainActive = true;
        movement.ContextSpeedStack.Clear();

        freezeTimer = 0f;
        slowTimer = 0f;
        damageOverTimeTimer = 0f;
    }

    private void Update()
    {
        // Freeze
        if (freezeTimer > 0f)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0f)
            {
                damageHandler.EnableOutgoingDamage();
                movement.ResetContextSpeedMultiplier();
                aiBrain.BrainActive = true;
            }
        }

        // Slow
        if (slowTimer > 0f)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0f)
            {
                movement.ResetContextSpeedMultiplier();
            }
        }

        // DamageOverTime
        if (damageOverTimeTimer > 0f)
        {
            if (Time.time - lastTickTime > tickRate)
            {
                damageHandler.ProcessIncomingDamage(damage);
                lastTickTime = Time.time;
            }
        }
    }

    // Plasma
    public void ApplyChaining(Damage damage, GameObject target)
    {
        target.GetComponent<DamageReceiver>().TakeDamage(damage);
    }

    // Cryo
    public void FreezeForDuration(float duration)
    {
        damageHandler.DisableOutgoingDamage();
        movement.SetContextSpeedMultiplier(0);
        aiBrain.BrainActive = false;
        freezeTimer = duration;
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
        slowTimer = duration;
    }

    // Pyro
    public void DamageOverTimeForDuration(Damage damage, float tickRate, float duration)
    {
        damageOverTimeTimer = duration;
        this.tickRate = tickRate;
        this.damage = damage;
    }
}
