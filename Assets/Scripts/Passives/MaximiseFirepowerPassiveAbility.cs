using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class MaximiseFirepowerPassiveAbility : PassiveAbility
    {
        public string AbilityName => abilityName;
        [SerializeField] private string abilityName = "Maximise Firepower";

        [SerializeField] private float[] damageMultipliers = new float[NUM_OF_TIERS + 1];

        [SerializeField] private DamageHandler damageHandler;

        public override void Upgrade()
        {
            currentTier++;
            damageHandler.SetOutgoingDamageMultiplier(damageMultipliers[currentTier]);
        }
    }
}
