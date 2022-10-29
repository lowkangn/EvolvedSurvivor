using UnityEngine;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class InfectStatusEffect : StatusEffect
    {
        private float duration;
        private readonly float magnitude = 0.3f;
        private int level;

        public override void Build(int level, float levelRatio, float utilityRatio, float maxMagnitude)
        {
            this.level = level;
            duration = levelRatio * utilityRatio * maxMagnitude;
        }

        public override void Apply(StatusEffectHandler handler, Damage damage)
        {
            handler.SlowForDuration(magnitude, duration);
        }

        public override string GetName()
        {
            return "Infect " + level;
        }
    }
}