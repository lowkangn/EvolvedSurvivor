using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class Projectile : DamageArea
    {
        private bool hasLifeTime = false;
        private float lifeTime;

        public void SetSize(float size)
        {
            transform.localScale = Vector3.one * size;
        }

        public void SetLifeTime(float lifeTime)
        {
            hasLifeTime = true;
            this.lifeTime = lifeTime;
        }

        private void Update()
        {
            if (hasLifeTime)
            {
                lifeTime -= Time.deltaTime;
                if (lifeTime < 0f)
                {
                    DisableDamageArea();
                }
            }
        }
    }
}
