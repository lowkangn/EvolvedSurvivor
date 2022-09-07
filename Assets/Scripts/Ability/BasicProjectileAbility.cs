using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System;

namespace TeamOne.EvolvedSurvivor
{
    public class BasicProjectileAbility : Ability
    {
        [SerializeField] private MMObjectPooler objectPool;
        private bool isActivated = false;

        // Public for now so can set values in inspector, later private when provided by Build()
        // Fire at closest target, knockback (based on speed)
        public int damage;
        public int pierceLimit;
        public int projectileCount;
        public float projectileSpeed;
        public float projectileSize;

        // Calculated during activation
        private Vector3 direction;

        public override void UpgradeAbility(Ability consumedAbility)
        {
            return;
        }

        protected override void Build(TraitChart traitChart)
        {
            return;
        }

        protected override void Activate()
        {
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
                    boxCollider.size = new Vector2(currX * projectileSize, currY * projectileSize);
                    isActivated = true;
                }

                Transform imageComponent = nextGameObject.transform.Find("Bullet");
                imageComponent.localScale = new Vector3(projectileSize, projectileSize, projectileSize);

                // Set damage
                DamageOnTouch damageOnTouch = nextGameObject.GetComponent<DamageOnTouch>();
                damageOnTouch.MinDamageCaused = damage;
                damageOnTouch.MaxDamageCaused = damage;

                // Set stats for the movement handler (sets projectileSpeed)
                BasicProjectileAbilityHandler handler = nextGameObject.GetComponent<BasicProjectileAbilityHandler>();
                handler.setSpeedAndDirection(projectileSpeed, direction);

                nextGameObject.SetActive(true);
            }
        }
    }
}
