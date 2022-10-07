using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class MaximiseFirepowerPassiveAbility : PassiveAbility
    {
        public string AbilityName => abilityName;
        [SerializeField] private string abilityName;

        [SerializeField] private float[] damageMultipliers = new float[NUM_OF_TIERS];

        public override void Upgrade()
        {
            currentTier++;
            DamageHandler damageHandler = transform.parent.gameObject.GetComponent<DamageHandler>();
            damageHandler.damageMultiplier = damageMultipliers[currentTier];
        }
    }
}
