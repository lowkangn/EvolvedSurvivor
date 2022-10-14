using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class MaximiseFirepowerPassiveAbility : PassiveAbility
    {
        [SerializeField] private float[] damageMultipliers = new float[NUM_OF_TIERS + 1];

        [SerializeField] private DamageHandler damageHandler;

        public override void Upgrade()
        {
            currentTier++;
            damageHandler.SetOutgoingDamageMultiplier(damageMultipliers[currentTier]);
        }

        protected override string GetStatsDescription()
        {
            return "Outgoing Damage Multiplication :" + damageMultipliers[currentTier] + "x";
        }
    }
}
