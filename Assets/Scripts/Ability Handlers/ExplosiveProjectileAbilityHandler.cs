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
                    StartCoroutine(kill());
                }
            }
        }

        // Sets the stats of the projectile and activates it
        public void SetStats(AbilityStat<float> damage, AbilityStat<float> aoeRadius, float projectileSpeed, Vector3 direction)
        {
            // Initialise various stats
            this.damage = damage.value;
            this.aoeRadius = aoeRadius.value;
            this.projectileSpeed = projectileSpeed;
            this.direction = direction;

            this.colliders = new List<Collider2D>();
            this.isHit = false;

            // Fixed at 0 for now (i.e. upon contact with one enemy, projectile dies), room for expansion in the future
            this.piercesLeft = 0;

            // Reset 'explosion'
            Transform explosion = gameObject.transform.Find("Explosion");
            explosion.localScale = new Vector3(0, 0, 0);

            // Initialise DamageArea
            DamageArea area = GetComponent<DamageArea>();
            Damage damageObj = new Damage(damage.value, gameObject, direction);
            area.SetDamage(damageObj);

            // Activate projectile
            isStatsSet = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag != "Enemy")
            {
                return;
            }

            // Some complication here: DamageArea component detects collision with the first enemy and deals damage accordingly, but we must manually damage the others in the AOE
            if (!colliders.Contains(other))
            {
                isHit = true;
                colliders.Add(other);

                Transform explosion = gameObject.transform.Find("Explosion");
                BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
                StartCoroutine(flash(explosion, collider));

                HashSet<GameObject> enemiesHit = new HashSet<GameObject>();

                Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(transform.position, aoeRadius, LayerMask.GetMask("Enemies"));
                foreach (Collider2D enemy in enemiesInRadius)
                {
                    if (enemy != other)
                    {
                        enemiesHit.Add(enemy.gameObject);
                    }
                }

                // For each enemy in aoeRadius that isn't the one that was initially hit, deal damage
                foreach (GameObject enemy in enemiesHit)
                {
                    if (enemy.tag == "Enemy")
                    {
                        Damage damageObj = new Damage(damage, gameObject, new Vector3(0, 0, 0));
                        DamageHandler handler = gameObject.GetComponent<DamageHandler>();
                        Damage processedDamageObj = handler.ProcessOutGoingDamage(damageObj);
                        DamageReceiver enemyReceiver = enemy.GetComponent<DamageReceiver>();
                        enemyReceiver.TakeDamage(processedDamageObj);
                    }
                }
            }
        }

        // Simulates the explosion: There's an explosion transform that's just a red circle, so when the 'explosion' happens we activate the circle by setting the size
        private IEnumerator flash(Transform explosion, BoxCollider2D collider)
        {
            explosion.localScale = new Vector3(aoeRadius / collider.size.x, aoeRadius / collider.size.y, 0);
            yield return new WaitForSeconds(0.1f);

            piercesLeft--;
        }
    }
}
