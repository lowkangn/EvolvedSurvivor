using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
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

        private void OnDestroy()
        {
            objectPool.DestroyObjectPool();
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
        }
    }
}
