using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class ConeAbility : Ability
    {
        [SerializeField]
        private AbilityStat<float> damage;
        [SerializeField]
        private AbilityStat<float> duration;
        [SerializeField]
        private AbilityStat<float> aoeRadius;
        [SerializeField]
        private AbilityStat<int> coneNumber;

        protected override void Activate()
        {
            print("shoot");
        }

        protected override void Build()
        {
            // Damage
            damage.value = (damage.maxValue - damage.minValue) * traitChart.DamageRatio + damage.minValue;

            // Uptime
            coolDown.value = coolDown.maxValue - (coolDown.maxValue - coolDown.minValue) * traitChart.UptimeRatio;
            duration.value = (duration.maxValue - duration.minValue) * traitChart.UptimeRatio + duration.minValue;

            // AOE
            aoeRadius.value = (aoeRadius.maxValue - aoeRadius.minValue) * traitChart.AoeRatio + aoeRadius.minValue;

            // Quantity
            coneNumber.value = Mathf.FloorToInt((coneNumber.maxValue - coneNumber.minValue) * traitChart.QuantityRatio + coneNumber.minValue);

            // Utility

        }
    }
}
