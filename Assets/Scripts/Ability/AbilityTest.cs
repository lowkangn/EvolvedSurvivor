using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class AbilityTest : MonoBehaviour
    {
        public AbilityGenerator generator;
        public int tier1;
        public int tier2;

        private void Start()
        {
            Ability a1 = generator.GenerateAbility(tier1);
            Ability a2 = generator.GenerateAbility(tier2);

            a1.UpgradeAbility(a2);
        }
    }
}
