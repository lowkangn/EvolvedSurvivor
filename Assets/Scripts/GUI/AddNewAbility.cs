using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoreMountains.Tools
{
    public class AddNewAbility : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject text;

        private GameObject abilitySlot;
        private bool canAddAbilities;

        void Start() {
            canAddAbilities = false;

            // Gets the first empty currentAbilities slot (Only can add to this)
            for (int i = 1; i < 7; i++) {
                abilitySlot = GameObject.Find("CurrentAbilities/Button" + i.ToString());
                if (FindChildWithTag(abilitySlot, "AbilitySprite") == null) {
                    canAddAbilities = true;
                    break;
                }
            }
        }

        //Detect if the Cursor starts to pass over the button
        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            text.GetComponent<Text>().text = "Description";
        }

        //Detect when Cursor leaves the button
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            text.GetComponent<Text>().text = "";
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (canAddAbilities) {
                GameObject ability = FindChildWithTag(this.gameObject, "AbilitySprite");
                // Transform ability = transform.Find("Ability");
                abilitySlot.GetComponent<AddAbility>().AddAbilityToCurrent(ability);
            } else {
                text.GetComponent<Text>().text = "Current Abilities are full.";
            }
        }

        GameObject FindChildWithTag(GameObject parent, string tag) {
            GameObject child = null;
            
            foreach(Transform transform in parent.transform) {
                if(transform.CompareTag(tag)) {
                    child = transform.gameObject;
                    break;
                }
            }
            
            return child;
        }
    }
}

