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

        protected override void Activate()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] onScreenEnemies = enemies.Where(x => GeneralUtility.IsOnScreen(x)).ToArray();
            HashSet<GameObject> enemiesHit = new HashSet<GameObject>();
            if (onScreenEnemies.Length > 0)
            {
                // Shuffle the onScreenEnemies array
                GeneralUtility.ShuffleArray(ref onScreenEnemies);

                // pick random enemies from shuffled array up to targetNumber 
                for (int i = 0; i < targetNumber.value && i < onScreenEnemies.Length; i++)
                {
                    // Choose an enemy and spawn projectile on top of it
                    GameObject chosenEnemy = onScreenEnemies[i];
                    DamageAndSpawnProjectileOnTarget(chosenEnemy);
                }
            }
        }

        private void DamageAndSpawnProjectileOnTarget(GameObject target)
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
    }
}
