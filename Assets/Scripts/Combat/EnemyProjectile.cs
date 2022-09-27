using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class EnemyProjectile : Projectile
    {
        private Vector3 direction;
        private float speed;

        private void Update()
        {
            transform.Translate(speed * Time.deltaTime * direction);
        }

        public void SetDirection(Vector3 direction)
        {
            this.direction = direction;
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }
    }
}

