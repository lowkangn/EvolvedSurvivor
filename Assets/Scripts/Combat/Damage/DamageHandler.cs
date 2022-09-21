using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
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
            // TODO: Process damage reduction, force application, debuffs, etc

            // Reduce health
            health.Damage(damage.damage, damage.instigator, 0, invincibilityDurationAfterTakingDamage, damage.direction);
        }

        /// <summary>
        /// Processes the out going damage
        /// </summary>
        /// <param name="damage">The base damage</param>
        /// <returns>The actual damage</returns>
        public Damage ProcessOutGoingDamage(Damage damage)
        {
            // TODO: Process passive abilities such as Global Damage Up

            damage.instigator = gameObject;

            return damage;
        }
    }
}
