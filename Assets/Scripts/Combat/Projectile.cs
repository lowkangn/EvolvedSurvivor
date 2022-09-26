using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class Projectile : DamageArea
    {
        private Vector3 motion;

        public void SetSize(float size)
        {
            transform.localScale = Vector3.one * size;
        }

        public void SetMotion(Vector2 motion)
        {
            this.motion = motion;
        }

        protected override void Update()
        {
            base.Update();
            transform.position += motion * Time.deltaTime;
        }
    }
}
