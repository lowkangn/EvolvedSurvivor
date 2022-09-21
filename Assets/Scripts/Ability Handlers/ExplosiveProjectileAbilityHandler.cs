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
        private int piercesLeft;
        private float projectileSpeed;
        private Vector3 direction;

        private List<Collider2D> colliders = new List<Collider2D>();

        void Update()
        {
            if (isStatsSet)
            {
                transform.Translate(direction * projectileSpeed * Time.deltaTime);

                // If projectile has reached its pierceLimit, deactivate it
                if (piercesLeft < 0)
                {
                    StartCoroutine(kill());
                }
            }
        }

        public void setStats(AbilityStat<float> damage, AbilityStat<float> aoeRadius, float projectileSpeed, Vector3 direction)
        {
            this.damage = damage.value;
            this.aoeRadius = aoeRadius.value;
            this.projectileSpeed = projectileSpeed;
            this.direction = direction;

            this.piercesLeft = 0;
            isStatsSet = true;
            colliders = new List<Collider2D>();

            Transform explosion = gameObject.transform.Find("Explosion");
            explosion.localScale = new Vector3(0, 0, 0);

        }

        // Used to count how many enemies contacted
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag != "Enemy")
            {
                return;
            }

            if (!colliders.Contains(other))
            {
                colliders.Add(other);

                Transform explosion = gameObject.transform.Find("Explosion");
                BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
                StartCoroutine(flash(explosion, collider));

                HashSet<GameObject> enemiesHit = new HashSet<GameObject>();

                Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(transform.position, aoeRadius, LayerMask.GetMask("Enemies"));
                foreach (Collider2D enemy in enemiesInRadius)
                {
                    enemiesHit.Add(enemy.gameObject);
                }

                foreach (GameObject enemy in enemiesHit)
                {
                    if (enemy.tag == "Enemy")
                    {
                        enemy.GetComponent<Health>().Damage(damage, gameObject, 0.5f, 0f, Vector3.zero);
                    }
                }
            }
        }

        private IEnumerator flash(Transform explosion, BoxCollider2D collider)
        {
            explosion.localScale = new Vector3(aoeRadius / collider.size.x, aoeRadius / collider.size.y, 0);
            yield return new WaitForSeconds(0.1f);

            piercesLeft--;
        }
    }
}
