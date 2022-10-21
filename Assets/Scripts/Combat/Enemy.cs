using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy stats")]
        [SerializeField] protected float initialDamage = 10f;
        [SerializeField] protected float initialHealth = 30f;

        [Header("Enemy components")]
        [SerializeField] protected DamageHandler damageHandler;
        [SerializeField] protected DamageArea damageArea;
        [SerializeField] protected Health health;
        [SerializeField] protected CharacterMovement movement;
        [SerializeField] protected AIBrain aiBrain;

        [Header("Spawn manager")]
        [SerializeField] private SpawnManagerScriptableObject spawnManager;

        protected void OnEnable()
        {
            this.health.Revive();
            damageHandler.ResetEffects();
            movement.ContextSpeedStack.Clear();
        }

        protected void OnDisable()
        {
            spawnManager.OnEnemyDespawn(this.gameObject);
        }

        public void FreezeForDuration(float duration)
        {
            damageHandler.DisableOutgoingDamageForDuration(duration);
            movement.ApplyMovementMultiplier(0, duration);
            aiBrain.BrainActive = false;
            StartCoroutine(EnableBrainAfterDuration(duration));
        }

        public void SlowForDuration(float magnitude,float duration)
        {
            movement.ApplyMovementMultiplier(magnitude, duration);
        }

        IEnumerator EnableBrainAfterDuration(float duration)
        {
            yield return new WaitForSeconds(duration);
            aiBrain.BrainActive = true;
        }

        public virtual void ScaleStats(float timePassed)
        {
            Damage damage = new Damage();
            damage.damage = this.initialDamage + (this.initialDamage * (Mathf.Log10(1f + (0.01f * timePassed))));

            damage = this.damageHandler.ProcessOutgoingDamage(damage);
            this.damageArea.SetDamage(damage);
            this.damageArea.SetActive(true);

            this.health.MaximumHealth = this.initialHealth + (this.initialHealth * (Mathf.Log10(1f + (0.015f * timePassed))));
            this.health.ResetHealthToMaxHealth();
        }
    }
}

