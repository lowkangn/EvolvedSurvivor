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

        public int tier;

        private void Start()
        {
            GenerateAbility(tier);
        }

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
            
            float damage = 0f;
            float uptime = 0f;
            float aoe = 0f;
            float quantity = 0f;
            float utility = 0f;

            while (totalTraitPoints > 0f) {
                float sum = damageRatio + uptimeRatio + aoeRatio + quantityRatio + utilityRatio;
                if (sum == 0)
                {
                    break;
                }
                float damageToAdd = Mathf.Clamp(damageRatio / sum * totalTraitPoints, 0, TraitChart.maxValue-damage);
                if (damageToAdd == 0) {
                    damageRatio = 0;    
                } 

                float uptimeToAdd = Mathf.Clamp(uptimeRatio / sum * totalTraitPoints, 0, TraitChart.maxValue-uptime);
                if (uptimeToAdd == 0) {
                    uptimeRatio = 0;
                } 

                float aoeToAdd = Mathf.Clamp(aoeRatio / sum * totalTraitPoints, 0, TraitChart.maxValue-aoe);
                if (aoeToAdd == 0) {
                    aoeRatio = 0;
                }

                float quantityToAdd = Mathf.Clamp(quantityRatio / sum * totalTraitPoints, 0, TraitChart.maxValue-quantity);
                if (quantityToAdd == 0) {
                    quantityRatio = 0;
                } 

                float utilityToAdd = Mathf.Clamp(utilityRatio / sum * totalTraitPoints, 0, TraitChart.maxValue-utility);
                if (utilityToAdd == 0) {
                    utilityRatio = 0;
                }

                damage += damageToAdd;
                uptime += uptimeToAdd;
                aoe += aoeToAdd;
                quantity += quantityToAdd;               
                utility += utilityToAdd;
                
                totalTraitPoints = totalTraitPoints - damageToAdd - uptimeToAdd - aoeToAdd - quantityToAdd - utilityToAdd;
            }

            TraitChart actualTraitChart = new TraitChart();
            actualTraitChart.damage = damage;
            actualTraitChart.uptime = uptime;
            actualTraitChart.aoe = aoe;
            actualTraitChart.quantity = quantity;
            actualTraitChart.utility = utility;
            Ability abilityInstance = Instantiate(chosenAbility);
            abilityInstance.BuildAbility(tier, actualTraitChart);

            return abilityInstance;
        }
    }
}
