using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class CellularRegenerationPassiveAbility : PassiveAbility
    {
        [SerializeField] private float[] healthMultipliers = new float[NUM_OF_TIERS + 1];
        [SerializeField] private float[] healthRegeneratedPerSecond = new float[NUM_OF_TIERS + 1];

        [SerializeField] private Health health;
        private float baseHealth;
        private float timeBeforeHeal;
        private float timer = 0;

        private bool isPreview;

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
            if (currentTier > 0 && health.CurrentHealth < health.MaximumHealth && !isPreview)
            {
                timer += Time.deltaTime;
                if (timer > timeBeforeHeal)
                {
                    health.ReceiveHealth(Mathf.Min(1f, health.MaximumHealth - health.CurrentHealth), gameObject);
                    timer -= timeBeforeHeal;
                }

            }
        }

        public override void UpgradeForPreview()
        {
            currentTier++;
            isPreview = true;
        }

        public override string GetDetails()
        {
            return "Max Health Up: " + healthMultipliers[currentTier] + "x\nRegeneration: +" + healthRegeneratedPerSecond[currentTier] + " Per Second\n";
        }
    }
}
