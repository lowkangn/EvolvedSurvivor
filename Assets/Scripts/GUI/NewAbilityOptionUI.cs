using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    // This class is added to the New Abilities on the Level Up screen
    public class NewAbilityOptionUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Text textObj;
        [SerializeField] private GameObject abilitySprite; // Ability in the NewAbility button
        [SerializeField] private Ability ability; 
        [SerializeField] private GameObject[] currentAbilities; // Current Abilities bar
        [SerializeField] private AddAbilityHandler lvlUpSystem;
        public int newAbilitySpriteSize = 200;

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
                abilitySlot.GetComponent<CurrentAbilityUI>().AddAbilityToCurrent(ability.sprite);
                lvlUpSystem.SetCurrentSelectedAbility(ability);
            } else {
                textObj.text = "Current Abilities are full.";
            }
        }

        public void AddAbilityToButton(Ability ability) {
            
            this.ability = ability;
            this.abilitySprite = ability.sprite;
            // SetAbility(abilitySprite);
            // GameObject abilitySprite = ability.sprite;

            Transform abilityTransform = Instantiate(abilitySprite.transform);
            abilityTransform.SetParent(gameObject.transform);
            RectTransform rectTransform = abilityTransform.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.localPosition = new Vector3(rectTransform.anchoredPosition.x,
                            rectTransform.anchoredPosition.y, 0f); // To set z value to 0
            rectTransform.localScale = new Vector3(newAbilitySpriteSize, newAbilitySpriteSize, 1);
        }
        
        public void SetAbility(GameObject abilitySprite) {
            this.abilitySprite = abilitySprite;
        }
    }
}

