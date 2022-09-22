using System.Collections;
using UnityEngine;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class PyroStatusEffect : StatusEffect
    {
        private readonly float tickRate = 0.5f;
        private readonly float duration = 3f;
        [SerializeField]
        private float damageMultiplier;

        public override void Build(float levelRatio, float utilityRatio, float maxMagnitude)
        {
            damageMultiplier = levelRatio * utilityRatio * maxMagnitude;
        }

        public override void Apply(GameObject target, Damage damage)
        {
            Damage dot = new Damage(damage.damage * damageMultiplier, damage.instigator);
            target.GetComponent<DamageHandler>().ApplyDamageOverTime(dot, duration, tickRate);
        }

    }
}