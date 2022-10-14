using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class PassiveAbilityButton : UpgradableButton
    {
        [SerializeField] protected PassiveAbility passiveAbility;

        public virtual void AddPassiveAbilityToButton(PassiveAbility passiveAbility)
        {
            base.AddUpgradableToButton(passiveAbility);
            this.passiveAbility = passiveAbility;
        }

        public virtual void RemoveAbility()
        {
            if (!this.isEmpty)
            {
                base.RemoveUpgradable();
                this.passiveAbility = null;
            }
        }
    }
}

