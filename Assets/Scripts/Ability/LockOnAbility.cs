using UnityEngine;
using System.Linq;
using MoreMountains.TopDownEngine;
using System.Collections.Generic;
using MoreMountains.Tools;

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
            GameObject[] onScreenEnemies = enemies.Where(x => IsOnScreen(x.transform)).ToArray();
            HashSet<GameObject> enemiesHit = new HashSet<GameObject>();
            if (onScreenEnemies.Length > 0)
            {
                // Shuffle the onScreenEnemies array
                for (int n = onScreenEnemies.Length - 1; n > 0; n--)
                {
                    int k = Random.Range(0, n);
                    GameObject temp = onScreenEnemies[n];
                    onScreenEnemies[n] = onScreenEnemies[k];
                    onScreenEnemies[k] = temp;
                }
                for (int i = 0; i < targetNumber.value && i < onScreenEnemies.Length; i++)
                {
                    GameObject nextProjectile = objectPool.GetPooledGameObject();
                    // Choose an enemy and spawn projectile on top of it
                    GameObject chosenEnemy = onScreenEnemies[i];
                    nextProjectile.transform.parent = chosenEnemy.transform;
                    nextProjectile.transform.localPosition = new Vector3(0, 0.5f, 0);
                    nextProjectile.SetActive(true);

                    // hit the enemy and apply AOE
                    enemiesHit.Add(chosenEnemy);
                    Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(chosenEnemy.transform.position, aoeRadius.value, LayerMask.GetMask("Enemies"));
                    foreach (Collider2D enemy in enemiesInRadius)
                    {
                        enemiesHit.Add(enemy.gameObject);
                    }
                }

                // Iterate over hit enemies and damage all of them
                foreach (GameObject enemy in enemiesHit)
                {
                    if (enemy.tag == "Enemy")
                    {
                        enemy.GetComponent<Health>().Damage(damage.value, gameObject, 0.5f, 0f, Vector3.zero);
                    }
                }
            }
        }

        private bool IsOnScreen(Transform enemy)
        {
            Camera mainCamera = Camera.main;
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(enemy.transform.position);
            return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
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
