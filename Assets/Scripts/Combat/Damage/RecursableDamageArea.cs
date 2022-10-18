using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    // This class is meant for damage areas used in player abilities.
    public class RecursableDamageArea : DamageArea
    {
        protected Ability recursiveAbility;
        protected bool wasRecursiveUsed = false;

        private void OnDisable()
        {
            if (!wasRecursiveUsed && recursiveAbility != null)
            {
                recursiveAbility.SetActive(false);
            }
        }

        protected override void OnHit()
        {
            base.OnHit();
            SpawnRecursiveAbility();
        }

        public void AddRecursiveAbility(Ability ability)
        {
            this.recursiveAbility = ability;
            this.wasRecursiveUsed = false;
        }

        protected virtual void SpawnRecursiveAbility()
        {
            if (recursiveAbility != null && !wasRecursiveUsed)
            {
                recursiveAbility.SetActive(true);
                recursiveAbility.transform.position = transform.position;
                wasRecursiveUsed = true;
            }
        }
    }
}
