using UnityEngine;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    // This class is added to the New Upgradables on the Level Up screen
    public class NewUpgradableOptionUI : UpgradableButton
    {
        [SerializeField] private LevelUpScreenManager levelUpManager;
        [SerializeField] private AddUpgradableHandler addUpgradableHandler;

        [SerializeField] private RadarChartUI radarChart;

        public override void OnPointerClick(PointerEventData pointerEventData)
        {
            if (upgradable.IsAbility())
            {
                this.levelUpManager.RefreshCurrentUpgradables();
                this.levelUpManager.AddNewAbility((Ability)upgradable);
            }
            else if (upgradable.IsPassiveAbility())
            {
                PassiveAbility passiveAbility = (PassiveAbility)upgradable;
                this.levelUpManager.RefreshCurrentUpgradables();
                if (passiveAbility.IsFirstUpgrade())
                {
                    this.levelUpManager.AddNewPassiveAbility(passiveAbility);
                }
            }
            else
            {
                return;
            }
            
            this.addUpgradableHandler.SetCurrentSelectedUpgradable(upgradable);
        }

        // Detect if the Cursor starts to pass over the button
        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (!IsEmpty())
            {
                this.textObj.text = upgradable.GetName();
                this.detailedTextObj.text = upgradable.GetDescription();
            }

            radarChart.UpdateVisual(upgradable);
        }

        // Detect when Cursor leaves the button
        public override void OnPointerExit(PointerEventData eventData)
        {
            this.textObj.text = "";
            this.detailedTextObj.text = "";

            radarChart.ClearVisual();
        }
    }
}
