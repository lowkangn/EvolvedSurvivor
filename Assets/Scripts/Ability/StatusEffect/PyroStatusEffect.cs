using System.Collections;
using UnityEngine;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class PyroStatusEffect : StatusEffect
    {
        private readonly float tickRate = 0.5f;
        private readonly float duration = 3f;
        private float damageMultiplier;
        private int level;

        public override void Build(int level, float magnitude)
        {
            this.level = level;
            damageMultiplier = magnitude;
        }

        public override void Apply(StatusEffectHandler handler, Damage damage)
        {
            if (damageMultiplier > 0)
            {
                Damage dot = new Damage(damage.damage * damageMultiplier, damage.instigator);
                handler.DamageOverTimeForDuration(dot, tickRate, duration);
            }
        }

        public override string GetName()
        {
            return "Pyro " + level;
        }
    }
}