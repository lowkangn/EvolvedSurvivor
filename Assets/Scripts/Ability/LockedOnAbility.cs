using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class LockedOnAbility : Ability
    {
        // Fire at random target
        public float damage;
        public int targetCount;
        public float aoeRadius;

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
