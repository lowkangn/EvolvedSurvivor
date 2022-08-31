using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class AbilityManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerRef;
        private List<Ability> abilitiesList;
        private List<bool> isAbilityActive; 

        void Start()
        {
            isAbilityActive = new List<bool>(new bool[100]);
        }

        public void AddAbility(Ability newAbility)
        {
            for (var i = 0; i < isAbilityActive.Count; i++)
            {
                if (!isAbilityActive[i])
                {
                    addPlayerRef(newAbility);
                    abilitiesList[i] = newAbility;
                    isAbilityActive[i] = true;
                    break;
                }
            }
        }

        public void ReplaceAbility(Ability abilityToAdd, int abilityToReplace)
        {
            RemoveAbility(abilityToReplace);
            AddAbility(abilityToAdd);
        }

        private void RemoveAbility(int abilityToRemove)
        {
            abilitiesList[abilityToRemove] = null;
            isAbilityActive[abilityToRemove] = false;
        }

        private void addPlayerRef(Ability abilityToAdd)
        {
            abilityToAdd.addPlayerRef(playerRef);
        }
    }
}
