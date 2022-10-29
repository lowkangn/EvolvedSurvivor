using System.Collections;
using UnityEngine;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class ForceStatusEffect : StatusEffect
    {
        [SerializeField]
        private float force;

        public override void Build(float levelRatio, float utilityRatio, float maxMagnitude)
        {
            force = levelRatio * utilityRatio * maxMagnitude;
        }

        public override void Apply(StatusEffectHandler handler, Damage damage)
        {
            Vector3 direction = handler.transform.position - damage.instigator.transform.position;
            handler.ApplyForce(direction, force);
        }

        public override string GetName()
        {
            return "Force";
        }
    }
}