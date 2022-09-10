using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    [CreateAssetMenu(fileName = "New File", menuName = "ScriptableObjects/AbilityBaseTraits")]
    public class AbilityBaseTraitCharts : ScriptableObject
    {
        [System.Serializable]
        public class AbilityTraitChartMapping
        {
            public Ability ability;
            public TraitChart traitChart;
        }

        public List<AbilityTraitChartMapping> abilityBaseTraitCharts;

        public TraitChart GetAbilityBaseTraitChart(Ability ability)
        {
            foreach (AbilityTraitChartMapping abilityTraitChartMapping in abilityBaseTraitCharts)
            {
                if (ability.GetType().Equals(abilityTraitChartMapping.ability.GetType()))
                {
                    return abilityTraitChartMapping.traitChart;
                }
            }

            return new TraitChart();
        }
    }
}
