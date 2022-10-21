using MoreMountains.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class OrbitalAbility : Ability
    {
        [SerializeField]
        private AbilityStat<float> damage;
        [SerializeField]
        private AbilityStat<float> duration;
        [SerializeField]
        private AbilityStat<float> projectileSize;
        [SerializeField]
        private AbilityStat<int> orbitalNumber;

        public float radius = 3f;
        public float rotationSpeed = 120f;

        private List<RecursableDamageArea> projectiles = new List<RecursableDamageArea>();
        private float angularDisplacement = 0f;

        protected override void Update()
        {
            base.Update();

            angularDisplacement = (angularDisplacement + rotationSpeed * Time.deltaTime) % 360f;

            float angleBetween = 360f / orbitalNumber.value;

            for (int i = 0; i < projectiles.Count; i++)
            {
                float x = Mathf.Cos(Mathf.Deg2Rad * (angleBetween * i + angularDisplacement)) * radius;
                float y = Mathf.Sin(Mathf.Deg2Rad * (angleBetween * i + angularDisplacement)) * radius;
                projectiles[i].transform.localPosition = this.transform.position + new Vector3(x, y, 0);
            }
        }

        private void OnDisable()
        {
            projectiles = new List<RecursableDamageArea>();
        }

        protected override void Activate()
        {
            projectiles = new List<RecursableDamageArea>();
            angularDisplacement = 0f;

            float angleBetween = 360f / orbitalNumber.value;

            for (int i = 0; i < orbitalNumber.value; i++)
            {
                RecursableDamageArea projectile = projectileObjectPool.GetPooledGameObject().GetComponent<RecursableDamageArea>();
                projectiles.Add(projectile);
                projectile.SetActive(true);

                // Set damage
                Damage projDamage = new Damage(damage.value, gameObject, effects);
                projDamage = damageHandler.ProcessOutgoingDamage(projDamage);

                projectile.SetDamage(projDamage);
                projectile.SetSize(projectileSize.value);

                // Set duration
                projectile.SetLifeTime(duration.value);

                // Add recursive ability if it is recursive
                if (hasRecursive)
                {
                    Ability recursiveAbility = recursiveAbilityObjectPool.GetPooledGameObject().GetComponent<Ability>();
                    recursiveAbility.gameObject.SetActive(true);
                    projectile.AddRecursiveAbility(recursiveAbility);
                }    
            }
        }

        protected override void Build()
        {
            // Damage
            damage.value = (damage.maxValue - damage.minValue) * traitChart.DamageRatio + damage.minValue;

            // Uptime
            coolDown.value = coolDown.maxValue - (coolDown.maxValue - coolDown.minValue) * traitChart.UptimeRatio;
            duration.value = (duration.maxValue - duration.minValue) * traitChart.UptimeRatio + duration.minValue;

            // AOE
            projectileSize.value = (projectileSize.maxValue - projectileSize.minValue) * traitChart.AoeRatio + projectileSize.minValue;

            // Quantity
            orbitalNumber.value = Mathf.FloorToInt((orbitalNumber.maxValue - orbitalNumber.minValue) * traitChart.QuantityRatio + orbitalNumber.minValue);

            // Utility
            foreach (KeyValuePair<ElementType, int> el in element.elements)
            {
                if (el.Value > 0)
                {
                    effects.Add(GenerateEffect(el.Key, traitChart.UtilityRatio, elementMagnitudes[(int)el.Key]));
                }
            }
        }

        protected override void HandleRecursive()
        {
            if (!hasActivated)
            {
                Activate();
                hasActivated = true;
                Invoke("Deactivate", duration.value);
            }
        }

        protected override float DebuffTraitsForMerging(Ability other)
        {
            if (GetType() == other.GetType())
            {
                return 0f;
            }
            float points = other.traitChart.quantity * debuffFactor;
            other.traitChart.quantity -= points;
            return points;
        }

        protected override TraitChart CreateTraitChartForMerging(float pointsToAssign, bool isSameType)
        {
            float damageRatio = traitChart.damage;
            float uptimeRatio = traitChart.uptime;
            float aoeRatio = traitChart.aoe;
            float quantityRatio = traitChart.quantity;
            float utilityRatio = traitChart.utility;
            if (!isSameType)
            {
                quantityRatio = 0f;
            }
            pointsToAssign += traitChart.GetTotalPoints();
            float utilityBuff = pointsToAssign * buffFactor;
            pointsToAssign -= utilityBuff;
            float sum = damageRatio + uptimeRatio + aoeRatio + quantityRatio + utilityRatio;
            return new TraitChart(damageRatio / sum * pointsToAssign,
                uptimeRatio / sum * pointsToAssign,
                aoeRatio / sum * pointsToAssign,
                quantityRatio / sum * pointsToAssign,
                utilityRatio / sum * pointsToAssign + utilityBuff);
        }

        public override string GetDetails()
        {
            return $"{damage.value:0.0} damage per orb\n"
                + $"Fires every {coolDown.value:0.0} seconds\n"
                + $"Spawns {orbitalNumber.value} orbs\n"
                + $"Lasts {duration.value:0.0} seconds\n"
                + $"Orb radius: {projectileSize.value:0.0} units\n";
        }
    }
}
