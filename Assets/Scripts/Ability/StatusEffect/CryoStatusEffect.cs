using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class CryoStatusEffect : StatusEffect
    {
        private float duration;
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
                handler.FreezeForDuration(duration);
            }
        }

        public override string GetName()
        {
            return "Cryo " + level;
        }
    }
}