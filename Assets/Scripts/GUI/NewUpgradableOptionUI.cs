using UnityEngine;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    // This class is added to the New Upgradables on the Level Up screen
    public class NewUpgradableOptionUI : UpgradableButton<Upgradable>
    {
        [SerializeField] private LevelUpScreenManager levelUpManager;
        [SerializeField] private AddUpgradableHandler addUpgradableHandler;

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

            clickSfxHandler.PlaySfx();
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
