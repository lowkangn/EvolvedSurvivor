using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    // This class is added to the New Upgradables on the Level Up screen
    public class NewUpgradableOptionUI : UpgradableButton
    {
        [SerializeField] private LevelUpScreenManager levelUpManager;
        [SerializeField] private AddUpgradableHandler addUpgradableHandler;

        [SerializeField] private RadarChartUI radarChart;
        [SerializeField] private Text rcDamageText;
        [SerializeField] private Text rcUptimeText;
        [SerializeField] private Text rcAoeText;
        [SerializeField] private Text rcQuantityText;
        [SerializeField] private Text rcUtilityText;

        [SerializeField] private GameObject notAvailableBox;

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

            if (upgradable.IsAbility())
            {
                Ability ability = (Ability)upgradable;

                this.rcDamageText.text = ability.GetDamageDescription();
                this.rcUptimeText.text = ability.GetUptimeDescription();
                this.rcAoeText.text = ability.GetAoeDescription();
                this.rcQuantityText.text = ability.GetQuantityDescription();
                this.rcUtilityText.text = ability.GetUtilityDescription();

                radarChart.UpdateVisual(ability.GetTraitChart());
            }

            if (upgradable.IsPassiveAbility() && this.radarChart.gameObject.activeSelf)
            {
                this.notAvailableBox.SetActive(true);
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

            this.notAvailableBox.SetActive(false);
        }
    }
}
