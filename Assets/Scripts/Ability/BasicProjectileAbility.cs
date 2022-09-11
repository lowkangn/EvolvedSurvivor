using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class BasicProjectileAbility : Ability
    {
        [SerializeField]
        private AbilityStat<float> damage;
        [SerializeField]
        private AbilityStat<int> pierceLimit;
        [SerializeField]
        private AbilityStat<int> projectileNumber;
        [SerializeField]
        private AbilityStat<float> projectileSpeed;
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

            // AOE
            pierceLimit.value = Mathf.FloorToInt((pierceLimit.maxValue - pierceLimit.minValue) * traitChart.AoeRatio + pierceLimit.minValue);
            projectileSize.value = (projectileSize.maxValue - projectileSize.minValue) * traitChart.AoeRatio + projectileSize.minValue;

            // Quantity
            projectileNumber.value = Mathf.FloorToInt((projectileNumber.maxValue - projectileNumber.minValue) * traitChart.QuantityRatio + projectileNumber.minValue);

            // Utility
            projectileSpeed.value = (projectileSpeed.maxValue - projectileSpeed.minValue) * traitChart.UtilityRatio + projectileSpeed.minValue;
        }
    }
}
