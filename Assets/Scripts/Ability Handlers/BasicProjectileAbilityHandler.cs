using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class BasicProjectileAbilityHandler : AbilityHandler
    {
        private int piercesLeft;
        private float projectileSpeed;
        private Vector3 direction;

        private List<Collider2D> colliders = new List<Collider2D>();

        void Update()
        {
            if (isStatsSet)
            {
                transform.Translate(direction * projectileSpeed * Time.deltaTime);

                // If projectile has reached its pierceLimit, deactvivate it
                if (piercesLeft < 0)
                {
                    StartCoroutine(kill());
                }
            }
        }

        public void setStats(int pierceLimit, float projectileSpeed, Vector3 direction)
        {
            this.piercesLeft = pierceLimit;
            this.projectileSpeed = projectileSpeed;
            this.direction = direction;

            isStatsSet = true;
            colliders = new List<Collider2D>();
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
                piercesLeft--;
            }
        }
    }
}
