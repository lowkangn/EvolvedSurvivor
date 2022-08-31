using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class ConalAbility : Ability
    {
        // Fire forward, knockback?
        public float damagePerTick;
        public float aoeRadius; // angle of cone?
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