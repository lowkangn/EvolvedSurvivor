using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class Projectile : RecursableDamageArea
    {
        protected Vector3 motion;
        protected float speed;

        public void SetMotion(Vector2 motion)
        {
            this.speed = motion.magnitude;
            this.motion = motion.normalized;
            transform.up = this.motion;
        }

        protected override void Update()
        {
            base.Update();
            transform.position += motion * speed * Time.deltaTime;
        }
    }
}
