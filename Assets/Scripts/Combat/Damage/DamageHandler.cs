using MoreMountains.TopDownEngine;
using System.Collections;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class DamageHandler : MonoBehaviour
    {
        public float invincibilityDurationAfterTakingDamage;

        private float incomingDamageMultiplier = 1f;
        private float outgoingDamageMultiplier = 1f;

        private bool isDamageDisabled = false;

        private float damageDisabledDurationRemaining = 0f;

        private Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (isDamageDisabled)
            {
                damageDisabledDurationRemaining -= Time.deltaTime;
                if (damageDisabledDurationRemaining <= 0)
                {
                    isDamageDisabled = false;
                }
            }
        }

        public void ProcessIncomingDamage(Damage damage)
        {
            // Apply damage reduction
            damage.damage *= incomingDamageMultiplier;

            foreach (StatusEffect effect in damage.effects)
            {
                effect.Apply(gameObject, damage);
            }
            // Reduce health
            health.Damage(damage.damage, damage.instigator, 0, invincibilityDurationAfterTakingDamage, damage.direction);
        }

        public void ApplyDamageOverTime(Damage damage, float duration, float tickRate)
        {
            int ticks = Mathf.FloorToInt(duration / tickRate);
            damage.damage = damage.damage / ticks;
            StartCoroutine(DamageOverTimeCoroutine(damage, ticks, tickRate));
        }

        private IEnumerator DamageOverTimeCoroutine(Damage damage, int ticks, float tickRate)
        {
            int ticksRemaining = ticks;

            while (ticksRemaining > 0)
            {
                health.Damage(damage.damage, damage.instigator, 0, invincibilityDurationAfterTakingDamage, damage.direction);
                yield return new WaitForSeconds(tickRate);
                ticksRemaining--;
            }
        }

        public void DisableOutgoingDamageForDuration(float duration)
        {
            isDamageDisabled = true;
            if (damageDisabledDurationRemaining < duration)
            {
                damageDisabledDurationRemaining = duration;
            }
        }

        public void ResetEffects()
        {
            isDamageDisabled = false;
            damageDisabledDurationRemaining = 0;
            StopAllCoroutines();
        }

        /// <summary>
        /// Processes the out going damage
        /// </summary>
        /// <param name="damage">The base damage</param>
        /// <returns>The actual damage</returns>
        public Damage ProcessOutgoingDamage(Damage damage)
        {
            if (isDamageDisabled)
            {
                damage.damage = 0;
            }
            else
            {
                // Apply damage multiplier (global damage up)
                damage.damage *= outgoingDamageMultiplier;
            }

            damage.instigator = gameObject;

            return damage;
        }

        public void SetInvicibilityDuration(float newDuration)
        {
            invincibilityDurationAfterTakingDamage = newDuration;
        }

        public void SetIncomingDamageMultiplier(float newMultiplier)
        {
            incomingDamageMultiplier = newMultiplier;
        }

        public void SetOutgoingDamageMultiplier(float newMultiplier)
        {
            outgoingDamageMultiplier = newMultiplier;
        }
    }
}
