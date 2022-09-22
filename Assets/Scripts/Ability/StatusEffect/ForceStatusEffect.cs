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

        public override void Apply(GameObject target, Damage damage)
        {
            Vector3 direction = target.transform.position - damage.instigator.transform.position;
            target.GetComponent<TopDownController2D>().Impact(direction, force);
        }
    }
}