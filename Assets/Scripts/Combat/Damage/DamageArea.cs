using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TeamOne.EvolvedSurvivor
{
    public class DamageArea : MonoBehaviour
    {
        public List<string> targetTags;
        public bool disableOnHit = true;
        public bool repeating = false;
        public float repeatingRate = 0.5f;
        
        public UnityEvent OnHitEvent;

        [SerializeField]
        private Collider2D damageCollider;
        private Damage damage;
        private float lastRepeatingTime;
        private HashSet<GameObject> alreadyHit = new HashSet<GameObject>();

        private void Start()
        {
            damageCollider = GetComponent<Collider2D>();
        }

        public void SetDamage(Damage damage)
        {
            this.damage = damage;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void AddAlreadyHit(GameObject obj)
        {
            alreadyHit.Add(obj);
        }

        private void Collide(Collider2D collision)
        {
            if (!targetTags.Contains(collision.tag))
            {
                return;
            }

            OnHitEvent.Invoke();

            if (!alreadyHit.Contains(collision.gameObject))
            {
                DamageReceiver damageReceiver = collision.GetComponent<DamageReceiver>();
                damageReceiver?.TakeDamage(damage);
            }

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
