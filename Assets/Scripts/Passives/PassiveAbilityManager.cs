using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class PassiveAbilityManager : MonoBehaviour
    {
        [SerializeField]
        private List<PassiveAbility> abilities;
        private List<PassiveAbility> activeAbilities = new List<PassiveAbility>();
        private bool[] maxedOutAbilities;

        void Awake()
        {
            this.maxedOutAbilities = new bool[abilities.Count];
        }

        public void UpgradeAbility(PassiveAbility passiveAbility)
        {
            int index = abilities.FindIndex(a => a.PassiveName == passiveAbility.PassiveName);
            if (index != -1)
            {
                if (!abilities[index].HasBeenUpgraded())
                {
                    activeAbilities.Add(abilities[index]);
                }
                abilities[index].Upgrade();

                if (abilities[index].HasBeenMaxedOut())
                {
                    maxedOutAbilities[index] = true;
                }
            }
        }

        public List<PassiveAbility> GetActiveAbilities()
        {
            return activeAbilities;
        }

        public PassiveAbility GetRandomUpgradableAbility()
        {
            if (!IsAllPassivesMaxedOut())
            {
                int randomIndex = Random.Range(0, abilities.Count);
                for (int i = 0; i < abilities.Count; i++)
                {
                    int currentIndex = (randomIndex + i) % abilities.Count;
                    if (!maxedOutAbilities[currentIndex])
                    {
                        PassiveAbility passiveAbilityCopy = Instantiate(abilities[currentIndex]);
                        passiveAbilityCopy.UpgradeForPreview();
                        return passiveAbilityCopy;
                    }
                }
            }

            return null;
        }

        public bool IsAllPassivesMaxedOut()
        {
            return maxedOutAbilities.All(x => x);
        }


    }
}
