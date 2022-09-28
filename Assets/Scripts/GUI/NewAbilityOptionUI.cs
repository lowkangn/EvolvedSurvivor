using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    // This class is added to the New Abilities on the Level Up screen
    public class NewAbilityOptionUI : AbilityButton
    {
        [SerializeField] private LevelUpScreenManager levelUpManager;
        [SerializeField] private AddAbilityHandler addAbilityHandler;

        public override void OnPointerClick(PointerEventData pointerEventData)
        {
            this.levelUpManager.AddNewAbility(ability);
            this.addAbilityHandler.SetCurrentSelectedAbility(ability);
        }
    }
}
