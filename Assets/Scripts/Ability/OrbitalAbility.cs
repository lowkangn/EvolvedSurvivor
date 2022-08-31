using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class OrbitalAbility : Ability
    {
        // Follow transform, knockback (based on speed)
        public float damagePerTick;
        public int orbitalCount;
        public float aoeRadius; // radius not included in doc but orbitals have a radius no?
        public float duration;
        public float projectileSpeed;
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