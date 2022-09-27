using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class AbilityButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Text textObj;
        [SerializeField] protected Image abilityImage;
        [SerializeField] protected Ability ability;

        public void AddAbilityToButton(Ability ability)
        {

            this.ability = ability;
            this.abilityImage.sprite = ability.GetSprite();
        }

        public abstract void OnPointerClick(PointerEventData eventData);

        // Detect if the Cursor starts to pass over the button
        public void OnPointerEnter(PointerEventData eventData)
        {
            textObj.text = "Description";
        }

        // Detect when Cursor leaves the button
        public void OnPointerExit(PointerEventData eventData)
        {
            textObj.text = "";
        }
    }
}

