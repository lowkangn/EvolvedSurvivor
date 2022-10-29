using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class CryoStatusEffect : StatusEffect
    {
        private float duration;
        private int level;
        public override void Build(int level, float levelRatio, float utilityRatio, float maxMagnitude)
        {
            this.level = level;
            duration = levelRatio * utilityRatio * maxMagnitude;
        }

        public override void Apply(StatusEffectHandler handler, Damage damage)
        {
            handler.FreezeForDuration(duration);
        }

        public override string GetName()
        {
            return "Cryo " + level;
        }
    }
}