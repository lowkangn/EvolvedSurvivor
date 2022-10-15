using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    // This class is meant for damage areas used in player abilities.
    public class RecursableDamageArea : DamageArea
    {
        protected Ability recursiveAbility;
        private bool wasRecursiveUsed = false;

        protected override void OnHit()
        {
            base.OnHit();

            if (recursiveAbility != null && !wasRecursiveUsed)
            {
                recursiveAbility.SetActive(true);
                recursiveAbility.transform.position = transform.position;
                wasRecursiveUsed = true;
            }
        }

        public void AddRecursiveAbility(Ability ability)
        {
            this.recursiveAbility = ability;
            this.wasRecursiveUsed = false;
        }
    }
}
