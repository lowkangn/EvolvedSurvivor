using MoreMountains.TopDownEngine;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy stats")]
        [SerializeField] protected float initialDamage = 10f;
        [SerializeField] protected float initialHealth = 30f;

        [Header("Enemy components")]
        [SerializeField] protected DamageHandler damageHandler;
        [SerializeField] protected DamageArea damageArea;
        [SerializeField] protected Health health;

        protected void OnEnable()
        {
            this.health.Revive();
        }

        public virtual void ScaleStats(float timePassed)
        {
            Damage damage = new Damage();
            damage.damage = this.initialDamage + (this.initialDamage * (Mathf.Log10(1f + (0.01f * timePassed))));

            damage = this.damageHandler.ProcessOutgoingDamage(damage);
            this.damageArea.SetDamage(damage);
            this.damageArea.SetActive(true);

            this.health.MaximumHealth = this.initialHealth + (this.initialHealth * (Mathf.Log10(1f + (0.015f * timePassed))));
            this.health.ResetHealthToMaxHealth();
        }
    }
}

