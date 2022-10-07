using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class AbilityManager : MonoBehaviour
    {
        public List<Ability> Abilities { get; private set; } = new();
        [SerializeField]
        private AbilityGenerator abilityGenerator;
        [SerializeField]
        private int maxNumOfAbilities = 6;
        private float coolDownMultiplier = 1;

        public bool CanAddAbility()
        {
            return Abilities.Count < maxNumOfAbilities;
        }

        public bool AddAbility(Ability newAbility)
        {
            if (!CanAddAbility())
            {
                return false;
            }

            newAbility.SetOwner(transform, abilityGenerator);
            Abilities.Add(newAbility);
            newAbility.transform.SetParent(transform, false);
            newAbility.SetCoolDownMultiplier(coolDownMultiplier);
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

        public void UpdateCoolDownMultiplier(float newMultiplier)
        {
            coolDownMultiplier = newMultiplier;
            UpdateExistingAbilitiesCoolDownMultipliers(coolDownMultiplier);
        }

        public void UpdateExistingAbilitiesCoolDownMultipliers(float multiplier)
        {
            foreach (Ability ability in Abilities)
            {
                ability.SetCoolDownMultiplier(multiplier);
            }
        }
    }
}
