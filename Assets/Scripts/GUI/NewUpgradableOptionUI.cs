using UnityEngine;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    // This class is added to the New Upgradables on the Level Up screen
    public class NewUpgradableOptionUI : UpgradableButton<Upgradable>
    {
        [SerializeField] private LevelUpScreenManager levelUpManager;
        [SerializeField] private AddUpgradableHandler addUpgradableHandler;
        [SerializeField] private Animator animator;

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

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            this.animator.SetBool("isHovering", true);
        }


        public override void OnPointerExit(PointerEventData eventData)
        {
            this.textObj.text = "";
            this.detailedTextObj.text = "";

            radarChart.ClearVisual();
            this.animator.SetBool("isHovering", false);
        }

        public override void AddUpgradableToButton(Upgradable upgradable)
        {
            base.AddUpgradableToButton(upgradable);
            this.animator.SetFloat("upgradableIndex", upgradable.GetAnimatorIndex());
        }
    }
}
