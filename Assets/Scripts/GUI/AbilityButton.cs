using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class AbilityButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Text textObj;
        [SerializeField] protected Image abilityImage;
        [SerializeField] protected Ability ability;

        protected bool isEmpty = true;

        public bool IsEmpty()
        {
            return this.isEmpty;
        }

        public virtual void AddAbilityToButton(Ability ability)
        {

            this.ability = ability;
            this.abilityImage.gameObject.SetActive(true);
            this.abilityImage.sprite = ability.GetSprite();
            this.isEmpty = false;
        }

        public abstract void OnPointerClick(PointerEventData eventData);

        // Detect if the Cursor starts to pass over the button
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            this.textObj.text = "Description";
        }

        // Detect when Cursor leaves the button
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            this.textObj.text = "";
        }

        public virtual void RemoveAbility()
        {
            if (!this.isEmpty)
            {
                this.isEmpty = true;
                this.ability = null;
                this.abilityImage.sprite = null;
                this.abilityImage.gameObject.SetActive(false);
            }
        }
    }
}

