using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class BasicProjectileAbility : Ability
    {
        // Fire at closest target, knockback (based on speed)
        public float damage;
        public int pierceLimit;
        public int projectileCount;
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
