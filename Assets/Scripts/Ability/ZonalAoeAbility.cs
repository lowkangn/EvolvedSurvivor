using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class ZonalAoeAbility : Ability
    {
        // Spawn at position
        public float damagePerTick;
        public int targetCount;
        public float aoeRadius;
        public float duration;

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
