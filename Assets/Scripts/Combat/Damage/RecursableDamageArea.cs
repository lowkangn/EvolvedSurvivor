using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    // This class is meant for damage areas used in player abilities.
    public class RecursableDamageArea : DamageArea
    {
        protected Ability recursiveAbility;

        protected override void OnHit()
        {
            base.OnHit();

            if (recursiveAbility != null)
            {
                recursiveAbility.SetActive(true);
                recursiveAbility.transform.position = transform.position;
            }
        }

        public void AddRecursiveAbility(Ability ability)
        {
            this.recursiveAbility = ability;
        }
    }
}
