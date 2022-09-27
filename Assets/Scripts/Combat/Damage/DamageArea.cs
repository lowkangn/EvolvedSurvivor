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

        protected Damage damage;
        private bool hasLifeTime = false;
        private float lifeTime;
        private float lastRepeatingTime;
        private HashSet<GameObject> alreadyHit = new HashSet<GameObject>();

        public virtual void SetDamage(Damage damage)
        {
            this.damage = damage;
        }

        public virtual void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetSize(float size)
        {
            transform.localScale = Vector3.one * size;
        }

        public void SetLifeTime(float lifeTime)
        {
            hasLifeTime = true;
            this.lifeTime = lifeTime;
        }

        public void AddAlreadyHit(GameObject obj)
        {
            alreadyHit.Add(obj);
        }

        protected virtual void OnHit()
        {

        }

        private void Collide(Collider2D collision)
        {
            if (!targetTags.Contains(collision.tag))
            {
                return;
            }

            OnHitEvent.Invoke();
            OnHit();

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

        protected virtual void Update()
        {
            if (hasLifeTime)
            {
                lifeTime -= Time.deltaTime;
                if (lifeTime < 0f)
                {
                    SetActive(false);
                }
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
                lastRepeatingTime = Time.time;
            }
        }
    }
}
