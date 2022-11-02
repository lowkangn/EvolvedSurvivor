using UnityEngine;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class InfectStatusEffect : StatusEffect
    {
        private float duration;
        private readonly float magnitude = 0.3f;
        private int level;

        public override void Build(int level, float magnitude)
        {
            this.level = level;
            duration = magnitude;
        }

        public override void Apply(StatusEffectHandler handler, Damage damage)
        {
            if (duration > 0)
            {
                handler.SlowForDuration(magnitude, duration);
            }
        }

        public override string GetName()
        {
            return "Infect " + level;
        }
    }
}