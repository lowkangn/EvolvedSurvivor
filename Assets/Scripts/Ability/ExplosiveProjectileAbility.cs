using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class ExplosiveProjectileAbility : Ability
    {
        
        [SerializeField]
        private MMObjectPooler objectPool;
        [SerializeField]
        private AbilityStat<float> damage;
        [SerializeField]
        private AbilityStat<float> aoeRadius;
        [SerializeField]
        private AbilityStat<int> projectileNumber;
        [SerializeField]
        private AbilityStat<float> projectileSize;

        private const float ATTACK_RADIUS = 13.0f;
        private Vector3 direction;
        private bool hasColliderSizeBeenSet = false;

        protected override void Activate()
        {
            GameObject nextGameObject = objectPool.GetPooledGameObject();

            // Set start position to player position
            setStartPosition(nextGameObject);

            // Find nearest enemy (if exists) and calculate direction
            bool isEnemyFound = setDirectionIfEnemyFound();

            if (isEnemyFound)
            {
                // Set projectile size
                setProjectileSize(nextGameObject);

                // Set damage
                setDamage(nextGameObject);

                // Set stats for the AbilityHandler
                initialiseHandler(nextGameObject);

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
            objToSetPosition.transform.position = playerRef.transform.position;
        }

        private bool setDirectionIfEnemyFound()
        {
            Vector2 playerPos2D = new Vector2(playerRef.transform.position.x, playerRef.transform.position.y);
            Vector2 projectileRange2D = new Vector2(ATTACK_RADIUS, ATTACK_RADIUS); 
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(playerPos2D, projectileRange2D, 0f, LayerMask.GetMask("Enemies"));

            float nearestDist = -1f;
            Collider2D nearest;
            bool isEnemyFound = false;

            foreach (Collider2D currCollider in hitColliders)
            {
                Vector3 currDirection = currCollider.GetComponent<Transform>().position - playerRef.transform.position;
                float dist = currDirection.magnitude;
                if (nearestDist == -1f || dist < nearestDist)
                {
                    nearestDist = dist;
                    nearest = currCollider;
                    direction = Vector3.Normalize(currDirection);
                    if (!isEnemyFound) isEnemyFound = true;
                }
            }

            return isEnemyFound;
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

        private void setDamage(GameObject objToSetDamage)
        {
            DamageOnTouch damageOnTouch = objToSetDamage.GetComponent<DamageOnTouch>();
            damageOnTouch.MinDamageCaused = Mathf.FloorToInt(damage.value);
            damageOnTouch.MaxDamageCaused = Mathf.FloorToInt(damage.value);
        }

        private void initialiseHandler(GameObject objToInitialiseHandler)
        {
            BasicProjectileAbilityHandler handler = objToInitialiseHandler.GetComponent<BasicProjectileAbilityHandler>();
            //handler.setStats(pierceLimit, projectileSpeed, direction);
        }
    }
}
