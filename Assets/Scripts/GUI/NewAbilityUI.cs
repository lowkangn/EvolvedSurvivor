using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoreMountains.Tools
{
    // This class is added to the New Abilities on the Level Up screen
    public class NewAbilityUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Text textObj;
        [SerializeField] private GameObject ability; // Ability in the NewAbility button
        [SerializeField] private GameObject[] currentAbilities; // Current Abilities bar

        private GameObject abilitySlot; // First empty currentAbilities slot
        private bool canAddAbilities;

        void Start() {
            canAddAbilities = false;

            // Gets the first empty currentAbilities slot (Only can add to this)
            for (int i = 0; i < 6; i++) {
                if (currentAbilities[i].transform.childCount == 0) {
                    abilitySlot = currentAbilities[i];
                    canAddAbilities = true;
                    break;
                }
            }
        }

        //Detect if the Cursor starts to pass over the button
        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            textObj.text = "Description";
        }

        //Detect when Cursor leaves the button
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            textObj.text = "";
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (canAddAbilities) {
                abilitySlot.GetComponent<CurrentAbilityLevelUpUI>().AddAbilityToCurrent(ability);
            } else {
                textObj.text = "Current Abilities are full.";
            }
        }
    }
}

