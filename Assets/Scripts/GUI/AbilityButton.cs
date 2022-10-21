using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class AbilityButton : UpgradableButton
    {
        
        [SerializeField] protected Ability ability;
        [SerializeField] protected RadarChartUI radarChart;

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

                // Radar chart mesh
                radarChart.UpdateVisual(ability);
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

