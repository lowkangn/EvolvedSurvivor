using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class DamageArea : MonoBehaviour
    {
        public List<string> targetTags;
        public bool disableOnHit = true;
        public bool repeating = false;
        public float repeatingRate = 0.5f;
        
        public delegate void DamageAreaEvent();
        public DamageAreaEvent OnHit;

        private Collider damageCollider;
        private Damage damage;
        private float lastRepeatingTime;

        public void SetDamage(Damage damage)
        {
            this.damage = damage;
        }

        public void SetActive(bool active)
        {
            damageCollider.enabled = active;
        }

        private void Collide(Collider2D collision)
        {
            if (!targetTags.Contains(collision.tag))
            {
                return;
            }

            OnHit.Invoke();

            DamageReceiver damageReceiver = collision.GetComponent<DamageReceiver>();
            damageReceiver?.TakeDamage(damage);

            if (disableOnHit)
            {
                SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Collide(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!repeating)
            {
                return;
            }

            if (Time.time - lastRepeatingTime > repeatingRate)
            {
                Collide(collision);
            }
        }
    }
}
