using UnityEngine;
using System.Linq;
using MoreMountains.TopDownEngine;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEditor.Experimental.GraphView;

namespace TeamOne.EvolvedSurvivor
{
    public class LockOnAbility : Ability
    {
        [SerializeField]
        private MMObjectPooler objectPool;
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
                    SpawnProjectileOnTarget(chosenEnemy);

                    // hit the enemy and hit all enemies in AOE radius
                    enemiesHit.Add(chosenEnemy);
                    Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(chosenEnemy.transform.position, aoeRadius.value, LayerMask.GetMask("Enemies"));
                    foreach (Collider2D enemy in enemiesInRadius)
                    {
                        enemiesHit.Add(enemy.gameObject);
                    }
                }

                // Iterate over hit enemies and damage all of them
                DamageAllEnemies(enemiesHit);
            }
        }

        private void SpawnProjectileOnTarget(GameObject target)
        {
            GameObject nextProjectile = objectPool.GetPooledGameObject();
            nextProjectile.transform.parent = target.transform;
            nextProjectile.transform.localPosition = new Vector3(0, 0.5f, 0);
            nextProjectile.SetActive(true);
        }

        private void DamageAllEnemies(HashSet<GameObject> targets)
        {
            foreach (GameObject target in targets)
            {
                if (target.tag == "Enemy")
                {
                    target.GetComponent<Health>().Damage(damage.value, gameObject, 0.5f, 0f, Vector3.zero);
                    foreach (StatusEffect effect in effects)
                    {
                        effect.Apply(target);
                    }
                }
            }
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
            
        }
    }
}
