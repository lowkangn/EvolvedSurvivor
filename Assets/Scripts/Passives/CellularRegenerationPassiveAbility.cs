using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class CellularRegenerationPassiveAbility : PassiveAbility
    {
        public string AbilityName => abilityName;
        [SerializeField] private string abilityName = "Cellular Regeneration";

        [SerializeField] private float[] healthMultipliers = new float[NUM_OF_TIERS + 1];
        [SerializeField] private float[] healthRegeneratedPerSecond = new float[NUM_OF_TIERS + 1];

        [SerializeField] private Health health;
        private float baseHealth;
        private float timeBeforeHeal;
        private float timer = 0;

        void Start()
        {
            baseHealth = health.MaximumHealth;
        }

        public override void Upgrade()
        {
            currentTier++;
            health.MaximumHealth = baseHealth * healthMultipliers[currentTier];
            timeBeforeHeal = 1 / healthRegeneratedPerSecond[currentTier];
        }

        void Update()
        {
            if (currentTier > 0)
            {
                timer += Time.deltaTime;
                if (timer > timeBeforeHeal)
                {
                    health.CurrentHealth += 1;
                    timer -= timeBeforeHeal;
                }

            }
        }
    }
}
