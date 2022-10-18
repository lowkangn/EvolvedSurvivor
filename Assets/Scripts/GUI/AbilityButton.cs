using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class AbilityButton : UpgradableButton
    {
        
        [SerializeField] protected Ability ability;
        [SerializeField] protected RadarChartUI radarChart;
        [SerializeField] protected Text rcDamageText;
        [SerializeField] protected Text rcUptimeText;
        [SerializeField] protected Text rcAoeText;
        [SerializeField] protected Text rcQuantityText;
        [SerializeField] protected Text rcUtilityText;

        public virtual void AddAbilityToButton(Ability ability)
        {
            base.AddUpgradableToButton(ability);
            this.ability = ability;
        }

        // Detect if the Cursor starts to pass over the button
        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (!isEmpty)
            {
                this.textObj.text = ability.GetName();
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
        public override void OnPointerExit(PointerEventData eventData)
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
                base.RemoveUpgradable();
                this.ability = null;
            }
        }
    }
}

