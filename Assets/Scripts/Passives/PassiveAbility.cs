using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class PassiveAbility : MonoBehaviour, Upgradable
    {
        public string AbilityName => abilityName;
        [SerializeField] private string abilityName;

        protected const int NUM_OF_TIERS = 3;

        [SerializeField]
        protected int currentTier = 0;
        [SerializeField]
        private Sprite abilitySprite;

        public abstract void Upgrade();

        public bool CanUpgrade()
        {
            return currentTier < NUM_OF_TIERS;
        }

        public bool HasBeenUpgraded()
        {
            return currentTier > 0;
        }

        public bool IsFirstUpgrade()
        {
            return currentTier == 1;
        }

        public bool HasBeenMaxedOut()
        {
            return currentTier == NUM_OF_TIERS;
        }

        public Sprite GetSprite()
        {
            return this.abilitySprite;
        }

        public string GetName()
        {
            return $"Level {currentTier} {abilityName}\n";
        }

        public string GetDescription()
        {
            return $"Level {currentTier} {abilityName}\n" + GetStatsDescription();
        }

        protected abstract string GetStatsDescription();

        public bool IsAbility()
        {
            return false;
        }

        public bool IsPassiveAbility()
        {
            return true;
        }

        public virtual void UpgradeForPreview()
        {
            currentTier++;
        }

    }
}
