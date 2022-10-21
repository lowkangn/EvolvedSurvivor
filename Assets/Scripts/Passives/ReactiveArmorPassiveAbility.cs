using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class ReactiveArmorPassiveAbility : PassiveAbility
    {
        [SerializeField] private float[] damageReductionMultipliers = new float[NUM_OF_TIERS + 1];
        [SerializeField] private float[] invincibilityDurationMultipliers = new float[NUM_OF_TIERS + 1];

        [SerializeField] private DamageHandler damageHandler;
        private float baseInvincibilityDuration;

        void Start()
        {
            baseInvincibilityDuration = damageHandler.invincibilityDurationAfterTakingDamage;
        }

        public override void Upgrade()
        {
            currentTier++;
            damageHandler.SetIncomingDamageMultiplier(damageReductionMultipliers[currentTier]);
            damageHandler.invincibilityDurationAfterTakingDamage = baseInvincibilityDuration * invincibilityDurationMultipliers[currentTier];
        }

        public override string GetDetails()
        {
            return "Incoming Damage Reduction: " + damageReductionMultipliers[currentTier] + "x\nInvincibility Duration Up: " 
                + invincibilityDurationMultipliers[currentTier] + "x\n";
        }
    }
}
