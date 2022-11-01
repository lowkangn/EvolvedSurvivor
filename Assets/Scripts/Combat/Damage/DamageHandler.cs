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

        private Health health;
        [SerializeField] private StatusEffectHandler statusEffectHandler;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        public void ProcessIncomingDamage(Damage damage)
        {
            // Apply damage reduction
            damage.damage *= incomingDamageMultiplier;

            // Apply status effects if this object has a handler attached
            if (statusEffectHandler)
            {
                foreach (StatusEffect effect in damage.effects)
                {
                    effect.Apply(statusEffectHandler, damage);
                }
            }

            // Reduce health
            health.Damage(damage.damage, damage.instigator, 0, invincibilityDurationAfterTakingDamage, damage.direction);
        }

        public void DisableOutgoingDamage()
        {
            isDamageDisabled = true;
        }

        public void EnableOutgoingDamage()
        {
            isDamageDisabled = false;
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
