using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class LaserAbility : Ability
    {
        [SerializeField]
        private AbilityStat<float> damage;
        [SerializeField]
        private AbilityStat<float> duration;
        [SerializeField]
        private AbilityStat<int> laserNumber;
        [SerializeField]
        private AbilityStat<float> projectileSize;

        public override void UpgradeAbility(Ability consumedAbility)
        {
            throw new System.NotImplementedException();
        }

        protected override void Activate()
        {
            print("shoot");
        }

        protected override void Build(TraitChart traitChart)
        {
            // Damage
            damage.value = (damage.maxValue - damage.minValue) * traitChart.DamageRatio + damage.minValue;

            // Uptime
            coolDown.value = coolDown.maxValue - (coolDown.maxValue - damage.minValue) * traitChart.UptimeRatio;
            duration.value = (duration.maxValue - duration.minValue) * traitChart.UptimeRatio + duration.minValue;

            // AOE
            projectileSize.value = (projectileSize.maxValue - projectileSize.minValue) * traitChart.AoeRatio + projectileSize.minValue;

            // Quantity
            laserNumber.value = Mathf.FloorToInt((laserNumber.maxValue - laserNumber.minValue) * traitChart.QuantityRatio + laserNumber.minValue);

            // Utility

        }
    }
}
