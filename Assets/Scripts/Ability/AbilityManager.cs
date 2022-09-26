using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class AbilityManager : MonoBehaviour
    {
        public List<Ability> Abilities { get; private set; } = new();
        [SerializeField]
        private int maxNumOfAbilities = 6;

        public bool CanAddAbility()
        {
            return Abilities.Count >= maxNumOfAbilities;
        }

        public bool AddAbility(Ability newAbility)
        {
            if (CanAddAbility())
            {
                return false;
            }

            Abilities.Add(newAbility);
            newAbility.transform.SetParent(transform, false);
            return true;
        }

        public void RemoveAbility(Ability ability)
        {
            if (!Abilities.Remove(ability))
            {
                Debug.LogError("Cannot find " + ability.name + " ");
            }
            Destroy(ability.gameObject);
        }

        public void StopAllAbilities()
        {
            foreach (Ability ability in Abilities)
            {
                ability.Stop();
            }
        }
    }
}
