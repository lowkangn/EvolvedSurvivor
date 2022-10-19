using System.Collections;
using UnityEngine;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class CryoStatusEffect : StatusEffect
    {
        [SerializeField]
        private float duration;

        public override void Build(float levelRatio, float utilityRatio, float maxMagnitude)
        {
            duration = levelRatio * utilityRatio * maxMagnitude;
        }

        public override void Apply(GameObject target, Damage damage)
        {
            target.GetComponent<DamageHandler>().DisableOutgoingDamageForDuration(duration);
            target.GetComponent<CharacterMovement>().ApplyMovementMultiplier(0, duration);
        }
    }
}