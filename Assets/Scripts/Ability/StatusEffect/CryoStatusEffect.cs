using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class CryoStatusEffect : StatusEffect
    {
        private float duration;
        public override void Build(int level, float magnitude)
        {
            this.Level = level;
            duration = magnitude;
        }

        public override void Apply(StatusEffectHandler handler, Damage damage)
        {
            if (duration > 0)
            {
                handler.FreezeForDuration(duration);
            }
        }

        public override string GetName()
        {
            return "Cryo " + Level;
        }

        public override bool EqualTypeTo(StatusEffect other)
        {
            return other is CryoStatusEffect;
        }
    }
}