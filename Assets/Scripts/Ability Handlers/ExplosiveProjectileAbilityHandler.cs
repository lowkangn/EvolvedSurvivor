using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class ExplosiveProjectileAbilityHandler : AbilityHandler
    {
        private float damage;
        private float aoeRadius;
        private float colliderSize;
        private float projectileSpeed;
        private Vector3 direction;

        private int piercesLeft;
        private List<Collider2D> colliders = new List<Collider2D>();
        private bool isHit = false;

        void Update()
        {
            if (isStatsSet)
            {
                if (!isHit)
                {
                    transform.Translate(direction * projectileSpeed * Time.deltaTime);
                }

                // If projectile has reached its pierceLimit, deactivate it
                if (piercesLeft < 0)
                {
                    StartCoroutine(KillCoroutine());
                }
            }
        }

        // Sets the stats of the projectile and activates it
        public void SetStats(AbilityStat<float> damage, AbilityStat<float> aoeRadius, float colliderSize, float projectileSpeed, Vector3 direction)
        {
            // Initialise various stats
            this.damage = damage.value;
            this.aoeRadius = aoeRadius.value;
            this.colliderSize = colliderSize;
            this.projectileSpeed = projectileSpeed;
            this.direction = direction;

            this.colliders = new List<Collider2D>();
            this.isHit = false;

            // Fixed at 0 for now (i.e. upon contact with one enemy, projectile dies), room for expansion in the future
            this.piercesLeft = 0;

            // Reset 'explosion'
            Transform explosion = gameObject.transform.Find("Explosion");
            explosion.localScale = new Vector3(0, 0, 0);

            // Activate projectile
            isStatsSet = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag != "Enemy")
            {
                return;
            }

            // If collided, set up explosion object then activate
            if (!colliders.Contains(other))
            {
                isHit = true;
                colliders.Add(other);

                GameObject explosion = (gameObject.transform.Find("Explosion")).gameObject;

                // Set circle size
                explosion.transform.localScale = new Vector3(aoeRadius / colliderSize, aoeRadius / colliderSize, 0);

                // Set DamageArea
                DamageArea area = explosion.GetComponent<DamageArea>();
                DamageHandler handler = explosion.GetComponent<DamageHandler>();
                Damage damageObj = new Damage(damage, explosion);
                Damage processedDamageObj = handler.ProcessOutgoingDamage(damageObj);
                area.SetDamage(processedDamageObj);

                StartCoroutine(ActivateExplosionEffect(explosion));
            }
        }

        // Activates and deactivates AOE
        private IEnumerator ActivateExplosionEffect(GameObject explosion)
        {
            // Activate AOE
            explosion.SetActive(true);

            yield return new WaitForSeconds(0.1f);

            // Deactivate AOE
            explosion.SetActive(false);

            piercesLeft--;
        }
    }
}
