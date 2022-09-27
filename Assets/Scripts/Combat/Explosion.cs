using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class Explosion : DamageArea
    {
        [SerializeField]
        private float duration;

        public override void SetActive(bool active)
        {
            base.SetActive(active);
            if (active)
            {
                SetLifeTime(duration);
            }
        }

        public void SetExplosionRadius(float radius)
        {
            transform.localScale = Vector3.one * radius;
        }
    }
}
