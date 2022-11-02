using System.Collections;
using UnityEngine;
using MoreMountains.TopDownEngine;
using UnityEngine.InputSystem.Utilities;

namespace TeamOne.EvolvedSurvivor
{
    public class ForceStatusEffect : StatusEffect
    {
        private float force;
        private int level;

        public override void Build(int level, float magnitude)
        {
            this.level = level;
            force = magnitude;
        }

        public override void Apply(StatusEffectHandler handler, Damage damage)
        {
            if (force > 0)
            {
                Vector3 direction = handler.transform.position - damage.instigator.transform.position;
                handler.ApplyForce(direction, force);
            }
        }

        public override string GetName()
        {
            return "Force " + level;
        }
    }
}