using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class QuickChargePassiveAbility : PassiveAbility
    {
        public string AbilityName => abilityName;
        [SerializeField] private string abilityName = "Quick Charge";

        [SerializeField] private float[] coolDownMultipliers = new float[NUM_OF_TIERS + 1];

        [SerializeField] private AbilityManager abilityManager;

        public override void Upgrade()
        {
            currentTier++;
            abilityManager.UpdateCoolDownMultiplier(coolDownMultipliers[currentTier]);
        }
    }
}
