using MoreMountains.TopDownEngine;
using System.Collections;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class DamageHandler : MonoBehaviour
    {
        public float invincibilityDurationAfterTakingDamage;

        private Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        public void ProcessIncomingDamage(Damage damage)
        {
            Debug.Log(damage);
            Debug.Log(damage.effects);
            // TODO: Process damage reduction, force application, debuffs, etc
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

        IEnumerator DamageOverTimeCoroutine(Damage damage, int ticks, float tickRate)
        {
            int ticksRemaining = ticks;

            while (ticksRemaining > 0)
            {
                health.Damage(damage.damage, damage.instigator, 0, invincibilityDurationAfterTakingDamage, damage.direction);
                yield return new WaitForSeconds(tickRate);
                ticksRemaining--;
            }
        }

        /// <summary>
        /// Processes the out going damage
        /// </summary>
        /// <param name="damage">The base damage</param>
        /// <returns>The actual damage</returns>
        public Damage ProcessOutgoingDamage(Damage damage)
        {
            // TODO: Process passive abilities such as Global Damage Up

            damage.instigator = gameObject;

            return damage;
        }
    }
}
