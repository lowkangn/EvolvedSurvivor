using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class LaserAbility : Ability
    {
        // Fire at random target, follow transform, pierce all, knockback
        public float damagePerTick;
        public int projectileCount;
        public float duration;
        public float projectileSize;

        public override void UpgradeAbility(Ability consumedAbility)
        {

        }

        protected override void Build(TraitChart traitChart)
        {

        }

        protected override void Activate()
        {

        }
    }
}
