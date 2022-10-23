using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class LockOnAbility : Ability
    {
        [SerializeField]
        private AbilityStat<float> damage;
        [SerializeField]
        private AbilityStat<float> aoeRadius;
        [SerializeField]
        private AbilityStat<int> targetNumber;

        [Header("The target detector for aiming")]
        [SerializeField]
        private TargetDetector targetDetector;

        protected override void Activate()
        {
            List<Transform> onScreenEnemies = targetDetector.ScanTargets();
            if (onScreenEnemies.Count > 0)
            {
                // pick random enemies from shuffled array up to targetNumber 
                for (int i = 0; i < targetNumber.value && i < onScreenEnemies.Count; i++)
                {
                    // Choose an enemy and spawn projectile on top of it
                    Transform chosenEnemy = onScreenEnemies[i];
                    DamageAndSpawnProjectileOnTarget(chosenEnemy);
                }
            }
        }

        private void DamageAndSpawnProjectileOnTarget(Transform target)
        {
            LockOnDamageArea nextProjectile = projectileObjectPool.GetPooledGameObject().GetComponent<LockOnDamageArea>();
            Damage projDamage = new Damage(damage.value, gameObject, effects);
            projDamage = damageHandler.ProcessOutgoingDamage(projDamage);
            nextProjectile.SetDamage(projDamage);
            nextProjectile.SetExplosionRadius(aoeRadius.value);
            nextProjectile.transform.parent = target.transform;
            nextProjectile.transform.localPosition = Vector3.zero;
            
            target.GetComponent<DamageReceiver>().TakeDamage(projDamage);

            if (hasRecursive)
            {
                Ability recursiveAbility = recursiveAbilityObjectPool.GetPooledGameObject().GetComponent<Ability>();
                recursiveAbility.gameObject.SetActive(true);
                nextProjectile.AddRecursiveAbility(recursiveAbility);
            }

            nextProjectile.SetActive(true);
        }

        protected override void Build()
        {
            // Damage
            damage.value = (damage.maxValue - damage.minValue) * traitChart.DamageRatio + damage.minValue;

            // Uptime
            coolDown.value = coolDown.maxValue - (coolDown.maxValue - coolDown.minValue) * traitChart.UptimeRatio;

            // AOE
            aoeRadius.value = (aoeRadius.maxValue - aoeRadius.minValue) * traitChart.AoeRatio + aoeRadius.minValue;

            // Quantity
            targetNumber.value = Mathf.FloorToInt((targetNumber.maxValue - targetNumber.minValue) * traitChart.QuantityRatio + targetNumber.minValue);

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
            Activate();
            Deactivate();
        }

        protected override float DebuffTraitsForMerging(Ability other)
        {
            if (GetType() == other.GetType())
            {
                return 0f;
            }
            float points = other.traitChart.aoe * debuffFactor;
            other.traitChart.aoe -= points;
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
                aoeRatio = 0f;
            }
            pointsToAssign += traitChart.GetTotalPoints();
            float damageBuff = pointsToAssign * buffFactor;
            pointsToAssign -= damageBuff;
            float sum = damageRatio + uptimeRatio + aoeRatio + quantityRatio + utilityRatio;
            return new TraitChart(damageRatio / sum * pointsToAssign + damageBuff,
                uptimeRatio / sum * pointsToAssign,
                aoeRatio / sum * pointsToAssign,
                quantityRatio / sum * pointsToAssign,
                utilityRatio / sum * pointsToAssign);
        }

        public override string GetDetails()
        {
            return $"{damage.value:0.0} damage on hit\n"
                + $"Fires every {coolDown.value:0.0} seconds\n"
                + $"Targets up to {targetNumber.value} enemies\n"
                + $"Explosion radius on strike: {aoeRadius.value:0.0} units\n"
                + "\n"
                + GetStatusEffects();
        }
    }
}
