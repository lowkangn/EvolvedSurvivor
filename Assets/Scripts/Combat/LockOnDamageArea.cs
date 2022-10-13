using MoreMountains.Tools;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class LockOnDamageArea : RecursableDamageArea
    {
        [Header("Explosion pool")]
        [SerializeField]
        private MMObjectPooler objectPool;

        private Damage explosionDamage;
        private float explosionRadius;
        private void OnEnable()
        {
            SetLifeTime(0.3f);
            OnHit();
        }
        public override void SetDamage(Damage damage)
        {
            this.damage = new Damage(0, null);
            explosionDamage = damage;
        }

        public void SetExplosionRadius(float radius)
        {
            explosionRadius = radius;
        }

        protected override void OnHit()
        {
            base.OnHit();
            Explosion explosion = objectPool.GetPooledGameObject().GetComponent<Explosion>();
            explosion.transform.position = transform.position;
            explosion.SetDamage(explosionDamage);
            explosion.SetExplosionRadius(explosionRadius);
            explosion.SetActive(true);
            explosion.AddTargetHit(transform.parent.gameObject);
        }
    }
}