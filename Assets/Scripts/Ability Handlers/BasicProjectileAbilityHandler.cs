using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class BasicProjectileAbilityHandler : AbilityHandler
    {
        private bool isStatsSet;
        private float projectileSpeed;
        private Vector3 direction;

        void Update()
        {
            if (isStatsSet)
            {
                transform.Translate(direction * projectileSpeed * Time.deltaTime);
            }
        }

        public override void setStats(Ability ability)
        {
            
        }

        public void setSpeedAndDirection(float projectileSpeed, Vector3 direction)
        {
            this.projectileSpeed = projectileSpeed;
            this.direction = direction;

            isStatsSet = true;
        }
    }
}
