using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class AbilityButton : UpgradableButton<Ability>
    {
        public virtual void AddAbilityToButton(Ability ability)
        {
            base.AddUpgradableToButton(ability);
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
            }
        }
    }
}

