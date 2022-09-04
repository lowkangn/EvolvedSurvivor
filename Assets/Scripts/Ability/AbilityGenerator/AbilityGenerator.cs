using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class AbilityGenerator : MonoBehaviour
    {
        [SerializeField]
        private List<Ability> abilityPrefabs;
        [SerializeField]
        private AbilityBaseTraitCharts abilityBaseTraitCharts;

        public Ability GenerateAbility(int tier)
        {
            Ability chosenAbility = abilityPrefabs[Random.Range(0, abilityPrefabs.Count)];
            TraitChart baseTraitChart = abilityBaseTraitCharts.GetAbilityBaseTraitChart(chosenAbility);

            float totalTraitPoints = 5 * tier;

            float damageRatio = Random.Range(0f, 1f) * baseTraitChart.damage;
            float uptimeRatio = Random.Range(0f, 1f) * baseTraitChart.uptime;
            float aoeRatio = Random.Range(0f, 1f) * baseTraitChart.aoe;
            float quantityRatio = Random.Range(0f, 1f) * baseTraitChart.quantity;
            float utilityRatio = Random.Range(0f, 1f) * baseTraitChart.utility;

            float sum = damageRatio + uptimeRatio + aoeRatio + quantityRatio + utilityRatio;

            TraitChart actualTraitChart = new TraitChart();
            actualTraitChart.damage = damageRatio / sum * totalTraitPoints;
            actualTraitChart.uptime = uptimeRatio / sum * totalTraitPoints;
            actualTraitChart.aoe = aoeRatio / sum * totalTraitPoints;
            actualTraitChart.quantity = quantityRatio / sum * totalTraitPoints;
            actualTraitChart.utility = utilityRatio / sum * totalTraitPoints;

            Ability abilityInstance = Instantiate(chosenAbility);
            abilityInstance.BuildAbility(actualTraitChart);

            return abilityInstance;
        }
    }
}
