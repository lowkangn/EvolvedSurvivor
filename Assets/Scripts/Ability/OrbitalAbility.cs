using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class OrbitalAbility : Ability
    {
        [SerializeField]
        private AbilityStat<float> damage;
        [SerializeField]
        private AbilityStat<float> duration;
        [SerializeField]
        private AbilityStat<float> projectileSize;
        [SerializeField]
        private AbilityStat<int> orbitalNumber;

        public float radius = 3f;
        private MMObjectPooler objectPool;

        private void Start()
        {
            objectPool = GetComponent<MMObjectPooler>();
        }

        protected override void Activate()
        {
            float angleBetween = 360f / orbitalNumber.value;

            for (int i = 0; i < orbitalNumber.value; i++)
            {
                Projectile projectile = objectPool.GetPooledGameObject().GetComponent<Projectile>();
                projectile.gameObject.SetActive(true);

                // Attach to ability transform
                projectile.transform.SetParent(transform);

                // Set damage
                Damage damage = new Damage();
                damage.damage = this.damage.value;
                // TODO: process damage with DamageHandler
                projectile.SetDamage(damage);
                projectile.SetSize(projectileSize.value);

                // Set duration
                projectile.SetLifeTime(duration.value);

                // Set local position
                float x = Mathf.Cos(Mathf.Deg2Rad * angleBetween * i) * radius;
                float y = Mathf.Sin(Mathf.Deg2Rad * angleBetween * i) * radius;
                projectile.transform.localPosition = new Vector2(x, y);
            }
        }

        protected override void Build()
        {
            // Damage
            damage.value = (damage.maxValue - damage.minValue) * traitChart.DamageRatio + damage.minValue;

            // Uptime
            coolDown.value = coolDown.maxValue - (coolDown.maxValue - coolDown.minValue) * traitChart.UptimeRatio;
            duration.value = (duration.maxValue - duration.minValue) * traitChart.UptimeRatio + duration.minValue;

            // AOE
            projectileSize.value = (projectileSize.maxValue - projectileSize.minValue) * traitChart.AoeRatio + projectileSize.minValue;

            // Quantity
            orbitalNumber.value = Mathf.FloorToInt((orbitalNumber.maxValue - orbitalNumber.minValue) * traitChart.QuantityRatio + orbitalNumber.minValue);

            // Utility

        }
    }
}
