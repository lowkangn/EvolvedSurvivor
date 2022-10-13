using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class Explosion : RecursableDamageArea
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
