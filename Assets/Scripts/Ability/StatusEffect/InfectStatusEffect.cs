using UnityEngine;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class InfectStatusEffect : StatusEffect
    {
        private float duration;
        private readonly float magnitude = 0.3f;

        public override void Build(int level, float magnitude)
        {
            this.Level = level;
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
            return "Infect " + Level;
        }

        public override bool EqualTypeTo(StatusEffect other)
        {
            return other is InfectStatusEffect;
        }
    }
}