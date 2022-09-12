using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class BasicProjectileAbility : Ability
    {
        [SerializeField] private MMObjectPooler objectPool;
        private bool isActivated = false;

        [SerializeField]
        private AbilityStat<float> damage;
        [SerializeField]
        private AbilityStat<int> pierceLimit;
        [SerializeField]
        private AbilityStat<int> projectileNumber;
        [SerializeField]
        private AbilityStat<float> projectileSpeed;
        [SerializeField]
        private AbilityStat<float> projectileSize;

        // Calculated during activation
        private Vector3 direction;

        public override void UpgradeAbility(Ability consumedAbility)
        {
            throw new System.NotImplementedException();
        }

        protected override bool Activate()
        {
            Debug.Log("Activate");
            GameObject nextGameObject = objectPool.GetPooledGameObject();

            // Set start position to player position
            nextGameObject.transform.position = playerRef.transform.position;

            // Find nearest enemy (if exists) and calculate direction
            Vector2 playerPos2D = new Vector2(playerRef.transform.position.x, playerRef.transform.position.y);
            Vector2 projectileRange2D = new Vector2(26.2f, 13.8f);
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

            if (isEnemyFound)
            {
                // Set projectile size
                if (!isActivated)
                {
                    BoxCollider2D boxCollider = nextGameObject.GetComponent<BoxCollider2D>();
                    float currX = boxCollider.size.x;
                    float currY = boxCollider.size.y;
                    boxCollider.size = new Vector2(currX * projectileSize.value, currY * projectileSize.value);
                    isActivated = true;
                }

                Transform imageComponent = nextGameObject.transform.Find("Bullet");
                imageComponent.localScale = new Vector3(projectileSize.value, projectileSize.value, projectileSize.value);

                // Set damage
                DamageOnTouch damageOnTouch = nextGameObject.GetComponent<DamageOnTouch>();
                damageOnTouch.MinDamageCaused = Mathf.FloorToInt(damage.value);
                damageOnTouch.MaxDamageCaused = Mathf.FloorToInt(damage.value);

                // Set stats for the movement handler (sets projectileSpeed)
                BasicProjectileAbilityHandler handler = nextGameObject.GetComponent<BasicProjectileAbilityHandler>();
                handler.setStats(pierceLimit.value, projectileSpeed.value, direction);

                nextGameObject.SetActive(true);
            }

            return isEnemyFound;
        }

        protected override void Build(TraitChart traitChart)
        {
            // Damage
            damage.value = (damage.maxValue - damage.minValue) * traitChart.DamageRatio + damage.minValue;

            // Uptime
            coolDown.value = coolDown.maxValue - (coolDown.maxValue - coolDown.minValue) * traitChart.UptimeRatio;

            // AOE
            pierceLimit.value = Mathf.FloorToInt((pierceLimit.maxValue - pierceLimit.minValue) * traitChart.AoeRatio + pierceLimit.minValue);
            projectileSize.value = (projectileSize.maxValue - projectileSize.minValue) * traitChart.AoeRatio + projectileSize.minValue;

            // Quantity
            projectileNumber.value = Mathf.FloorToInt((projectileNumber.maxValue - projectileNumber.minValue) * traitChart.QuantityRatio + projectileNumber.minValue);

            // Utility
            projectileSpeed.value = (projectileSpeed.maxValue - projectileSpeed.minValue) * traitChart.UtilityRatio + projectileSpeed.minValue;
        }
    }
}
