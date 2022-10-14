using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class AbilityButton : UpgradableButton
    {
        [SerializeField] protected Ability ability;

        public virtual void AddAbilityToButton(Ability ability)
        {
            base.AddUpgradableToButton(ability);
            this.ability = ability;
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

