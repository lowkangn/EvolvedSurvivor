using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class AbilityManager : MonoBehaviour
    {
        private Ability[] abilitiesList = new Ability[6];
        private bool[] isAbilityActive = new bool[6];

        public void AddAbility(Ability newAbility)
        {
            for (var i = 0; i < 5; i++)
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
