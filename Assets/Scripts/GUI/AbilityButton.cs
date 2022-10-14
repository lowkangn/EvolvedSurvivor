using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class AbilityButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Text textObj;
        [SerializeField] protected Text detailedTextObj;
        [SerializeField] protected Image abilityImage;
        [SerializeField] protected Ability ability;
        [SerializeField] protected RadarChartUI radarChart;
        [SerializeField] protected Text rcDamageText;
        [SerializeField] protected Text rcUptimeText;
        [SerializeField] protected Text rcAoeText;
        [SerializeField] protected Text rcQuantityText;
        [SerializeField] protected Text rcUtilityText;

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
            if (!IsEmpty())
            {
                this.textObj.text = ability.GetAbilityName();
                // Detailed view
                this.detailedTextObj.text = ability.GetDescription();

                // Radar chart labels
                this.rcDamageText.text = ability.GetDamageDescription();
                this.rcUptimeText.text = ability.GetUptimeDescription();
                this.rcAoeText.text = ability.GetAoeDescription();
                this.rcQuantityText.text = ability.GetQuantityDescription();
                this.rcUtilityText.text = ability.GetUtilityDescription();

                // Radar chart mesh
                radarChart.UpdateVisual(ability.GetTraitChart());
            } 
            else
            {
                this.textObj.text = "";
            }
        }

        // Detect when Cursor leaves the button
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            this.textObj.text = "";
            this.detailedTextObj.text = "";

            this.rcDamageText.text = "Damage";
            this.rcUptimeText.text = "Uptime";
            this.rcAoeText.text = "AOE";
            this.rcQuantityText.text = "Quantity";
            this.rcUtilityText.text = "Utility";

            radarChart.ClearVisual();
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

