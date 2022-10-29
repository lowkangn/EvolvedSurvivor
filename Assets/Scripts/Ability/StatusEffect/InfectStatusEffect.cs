using UnityEngine;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class InfectStatusEffect : StatusEffect
    {
        [SerializeField]
        private float duration;
        private readonly float magnitude = 0.3f;

        public override void Build(float levelRatio, float utilityRatio, float maxMagnitude)
        {
            duration = levelRatio * utilityRatio * maxMagnitude;
        }

        public override void Apply(StatusEffectHandler handler, Damage damage)
        {
            handler.SlowForDuration(magnitude, duration);
        }

        public override string GetName()
        {
            return "Infect";
        }
    }
}