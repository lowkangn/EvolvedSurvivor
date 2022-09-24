using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class ZonalAbility : Ability
    {
        [Header("Projectile pool")]
        [SerializeField]
        private MMObjectPooler objectPool;

        [Header("Generated stats - set minValue and maxValue only")]
        [SerializeField]
        private AbilityStat<float> damage;
        [SerializeField]
        private AbilityStat<float> duration;
        [SerializeField]
        private AbilityStat<float> aoeRadius;
        [SerializeField]
        private AbilityStat<int> targetNumber;

        private float initialColliderRadius = 0;

        protected override void Activate()
        {
            List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
            List<GameObject> onScreenEnemies = enemies.Where(x => GeneralUtility.IsOnScreen(x)).ToList();
            List<Collider2D> enemyColliders = onScreenEnemies.ConvertAll(x => x.GetComponent<Collider2D>());
            List<Vector3> enemyPositions = enemyColliders.ConvertAll(x => x.GetComponent<Transform>().position); 

            if (onScreenEnemies.Count > 0)
            {
                List<Vector3> positions = GetSpawnPositions(enemyPositions);

                foreach (Vector3 position in positions)
                {
                    GameObject nextGameObject = objectPool.GetPooledGameObject();
                    Initialise(nextGameObject, position);
                    nextGameObject.SetActive(true);
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
            aoeRadius.value = (aoeRadius.maxValue - aoeRadius.minValue) * traitChart.AoeRatio + aoeRadius.minValue;

            // Quantity
            targetNumber.value = Mathf.FloorToInt((targetNumber.maxValue - targetNumber.minValue) * traitChart.QuantityRatio + targetNumber.minValue);

            // Utility

        }

        private void Initialise(GameObject obj, Vector3 position)
        {
            // Set position
            obj.transform.position = position;

            // Set radius
            CircleCollider2D collider = obj.GetComponent<CircleCollider2D>();

            if (initialColliderRadius == 0) {
                initialColliderRadius = collider.radius;
            }

            collider.radius = aoeRadius.value;
            Transform aoeCircle = obj.transform.Find("AoeCircle");
            aoeCircle.localScale = new Vector3(aoeRadius.value / initialColliderRadius, aoeRadius.value / initialColliderRadius, 1);

            // Set DamageArea
            DamageArea area = obj.GetComponent<DamageArea>();
            Damage damageObj = new Damage(damage.value, obj);
            DamageHandler damageHandler = obj.GetComponent<DamageHandler>();
            Damage processedDamageObj = damageHandler.ProcessOutgoingDamage(damageObj);
            area.SetDamage(processedDamageObj);

            // Set Handler
            ZonalAbilityHandler abilityHandler = obj.GetComponent<ZonalAbilityHandler>();
            abilityHandler.SetDuration(duration.value);
        }

        private List<Vector3> GetSpawnPositions(List<Vector3> enemyPositions)
        {
            List<Vector3> spawnPositions = new List<Vector3>();

            for (int i = 0; i < targetNumber.value; i++)
            {
                if (enemyPositions.Count != 0)
                {
                    Vector3 nearest = GetNearest(enemyPositions);
                    spawnPositions.Add(nearest);
                    enemyPositions = RemoveEnemiesAlreadyInAoe(enemyPositions, nearest);
                }
            }

            return spawnPositions;
        }

        private Vector3 GetNearest(List<Vector3> enemyPositions)
        {

            float nearestDist = -1f;
            Vector3 nearest = new Vector3(0, 0, 0);

            foreach (Vector3 position in enemyPositions)
            {
                Vector3 currDirection = position - transform.position;
                float dist = currDirection.magnitude;

                if (nearestDist == -1f || dist < nearestDist)
                {
                    nearestDist = dist;
                    nearest = position;
                }
            }

            return nearest;
        }

        private List<Vector3> RemoveEnemiesAlreadyInAoe(List<Vector3> enemyPositions, Vector3 aoeCenter)
        {
            List<Vector3> enemiesOutsideAoe = new List<Vector3>();

            foreach (Vector3 position in enemyPositions)
            {
                Vector3 distVector = position - aoeCenter;
                float dist = distVector.magnitude;

                if (dist > aoeRadius.value)
                {
                    enemiesOutsideAoe.Add(position);
                }
            }

            return enemiesOutsideAoe;
        }
    }
}
