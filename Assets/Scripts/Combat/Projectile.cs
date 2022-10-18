using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class Projectile : RecursableDamageArea
    {
        private Vector3 motion;

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
