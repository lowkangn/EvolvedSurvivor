using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class ExplosiveProjectileAbility : Ability
    {
        [Header("Projectile pool")]
        [SerializeField]
        private MMObjectPooler objectPool;

        [Header("Generated stats - set minValue and maxValue only")]
        [SerializeField]
        private AbilityStat<float> damage;
        [SerializeField]
        private AbilityStat<float> aoeRadius;
        [SerializeField]
        private AbilityStat<int> projectileNumber;
        [SerializeField]
        private AbilityStat<float> projectileSize;

        [Header("Fixed stats")]
        [SerializeField]
        private float projectileSpeed;

        private bool hasColliderSizeBeenSet = false;

        protected override void Activate()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] onScreenEnemies = enemies.Where(x => IsOnScreen(x.transform)).ToArray();

            if (onScreenEnemies.Length > 0)
            {
                GameObject nextGameObject = objectPool.GetPooledGameObject();

                // Set start position to player position
                setStartPosition(nextGameObject);

                // Find nearest enemy (if exists) and calculate direction
                Vector3 nearestDirection = setDirectionIfEnemyFound(onScreenEnemies);

                // Set projectile size
                setProjectileSize(nextGameObject);

                // Set stats for the AbilityHandler
                initialiseHandler(nextGameObject, nearestDirection);

                nextGameObject.SetActive(true);
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
            projectileSize.value = (projectileSize.maxValue - projectileSize.minValue) * traitChart.AoeRatio + projectileSize.minValue;

            // Quantity
            projectileNumber.value = Mathf.FloorToInt((projectileNumber.maxValue - projectileNumber.minValue) * traitChart.QuantityRatio + projectileNumber.minValue);

            // Utility

        }

        private void setStartPosition(GameObject objToSetPosition)
        {
            objToSetPosition.transform.position = transform.position;
        }

        private Vector3 setDirectionIfEnemyFound(GameObject[] onScreenEnemies)
        {
            Vector2 playerPos2D = new Vector2(transform.position.x, transform.position.y);
            Collider2D[] hitColliders = Array.ConvertAll(onScreenEnemies, x => x.GetComponent<Collider2D>());

            float nearestDist = -1f;
            Vector3 direction = new Vector3(0, 0, 0);
            Collider2D nearest;

            foreach (Collider2D currCollider in hitColliders)
            {
                Vector3 currDirection = currCollider.GetComponent<Transform>().position - transform.position;
                float dist = currDirection.magnitude;
                if (nearestDist == -1f || dist < nearestDist)
                {
                    nearestDist = dist;
                    nearest = currCollider;
                    direction = Vector3.Normalize(currDirection);
                }
            }

            return direction;
        }

        private void setProjectileSize(GameObject objToSetSize)
        {
            if (!hasColliderSizeBeenSet)
            {
                BoxCollider2D boxCollider = objToSetSize.GetComponent<BoxCollider2D>();
                float currX = boxCollider.size.x;
                float currY = boxCollider.size.y;
                boxCollider.size = new Vector2(currX * projectileSize.value, currY * projectileSize.value);
                hasColliderSizeBeenSet = true;
            }

            Transform imageComponent = objToSetSize.transform.Find("Bullet");
            imageComponent.localScale = new Vector3(projectileSize.value, projectileSize.value, projectileSize.value);
        }

        private void initialiseHandler(GameObject objToInitialiseHandler, Vector3 direction)
        {
            ExplosiveProjectileAbilityHandler handler = objToInitialiseHandler.GetComponent<ExplosiveProjectileAbilityHandler>();
            handler.setStats(damage, aoeRadius, projectileSpeed, direction);
        }

        private bool IsOnScreen(Transform enemy)
        {
            Camera mainCamera = Camera.main;
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(enemy.transform.position);
            return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        }
    }
}
