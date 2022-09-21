using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class AbilityManager : MonoBehaviour
    {
        [SerializeField]
        private const int MAX_NUM_OF_ABILITIES = 6;

        [SerializeField]
        private Ability[] abilitiesList = new Ability[MAX_NUM_OF_ABILITIES];
        private bool[] isAbilityActive = new bool[MAX_NUM_OF_ABILITIES];

        public void AddAbility(Ability newAbility)
        {
            for (var i = 0; i < MAX_NUM_OF_ABILITIES; i++)
            {
                if (!isAbilityActive[i])
                {
                    newAbility.addPlayerRef(gameObject);
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
    }
}
