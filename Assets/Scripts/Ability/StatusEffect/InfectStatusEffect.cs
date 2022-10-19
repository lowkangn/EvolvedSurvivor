using UnityEngine;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class InfectStatusEffect : StatusEffect
    {
        [SerializeField]
        private float duration;
        private readonly float magnitude = 0.5f;

        public override void Build(float levelRatio, float utilityRatio, float maxMagnitude)
        {
            duration = levelRatio * utilityRatio * maxMagnitude;
        }

        public override void Apply(GameObject target, Damage damage)
        {
            target.GetComponent<Enemy>().SlowForDuration(magnitude, duration);
        }
    }
}