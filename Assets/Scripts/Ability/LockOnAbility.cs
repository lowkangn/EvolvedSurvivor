using UnityEngine;
using System.Linq;
using System.Collections.Generic;

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
            GameObject nextProjectile = objectPool.GetPooledGameObject();
            nextProjectile.transform.parent = target.transform;
            nextProjectile.transform.localPosition = Vector3.zero;
            nextProjectile.GetComponent<DamageArea>().SetDamage(projDamage);
            nextProjectile.GetComponent<CircleCollider2D>().radius = aoeRadius.value;
            nextProjectile.GetComponent<LockOnAbilityHandler>().SetParticleRadius(aoeRadius.value);
            target.GetComponent<DamageReceiver>().TakeDamage(projDamage);
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
                    projDamage.effects.Add(GenerateEffect(el.Key, traitChart.UtilityRatio, elementMagnitudes[el.Key]));
                }
            }

            // Set up projDamage
            projDamage.damage = damage.value;
            projDamage.instigator = gameObject;
        }
    }
}
