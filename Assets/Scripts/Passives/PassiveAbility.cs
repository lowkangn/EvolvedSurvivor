using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class PassiveAbility : MonoBehaviour, Upgradable
    {
        public string PassiveName => passiveName;
        [SerializeField] private string passiveName;
        [SerializeField] private string passiveDescription;

        protected const int NUM_OF_TIERS = 3;

        [SerializeField]
        protected int currentTier = 0;
        [SerializeField]
        private Sprite abilitySprite;
        [SerializeField]
        private UpgradableAnimatorIndex animatorIndex;

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

        public int GetAnimatorIndex()
        {
            return (int)this.animatorIndex;
        }

        public Sprite GetRecursiveSprite()
        {
            return null;
        }

        public string GetName()
        {
            return $"Level {currentTier} {passiveName}\n";
        }

        public string GetDescription()
        {
            return passiveDescription;
        }

        public abstract string GetDetails();

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
