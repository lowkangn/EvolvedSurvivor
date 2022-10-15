using MoreMountains.Tools;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class ExplosiveProjectile : Projectile
    {
        [Header("Explosion pool")]
        [SerializeField]
        private MMObjectPooler objectPool;

        private Damage explosionDamage;
        private float explosionRadius;
        private bool hasHit;

        private void OnDestroy()
        {
            objectPool.DestroyObjectPool();
        }

        private void OnEnable()
        {
            hasHit = false;
        }

        private void OnDisable()
        {
            if (!hasHit && recursiveAbility != null)
            {
                recursiveAbility.SetActive(false);
            }
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
            hasHit = true;
            Explosion explosion = objectPool.GetPooledGameObject().GetComponent<Explosion>();
            explosion.transform.position = transform.position;
            explosion.SetDamage(explosionDamage);
            explosion.SetExplosionRadius(explosionRadius);
            explosion.SetActive(true);
        }
    }
}
