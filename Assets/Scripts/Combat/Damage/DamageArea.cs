using System.Collections.Generic;
using System.Linq;
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
        private float lastRepeatingTime = 0f;
        private HashSet<GameObject> targetsHit = new HashSet<GameObject>();

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

        protected virtual void OnHit()
        {

        }

        private void Collide(GameObject collision)
        {
            targetsHit.Add(collision);

            OnHitEvent.Invoke();
            OnHit();

            DamageReceiver damageReceiver = collision.GetComponent<DamageReceiver>();
            damageReceiver?.TakeDamage(damage);

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
        public void AddTargetHit(GameObject target)
        {
            targetsHit.Add(target);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (targetTags.Contains(collision.tag))
            {
                Collide(collision.gameObject);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!repeating || !targetTags.Contains(collision.tag))
            {
                return;
            }

            if (Time.time - lastRepeatingTime >= repeatingRate)
            {
                foreach (GameObject target in targetsHit.ToList())
                {
                    Collide(target);
                }
                lastRepeatingTime = Time.time;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (targetTags.Contains(collision.tag))
            {
                targetsHit.Remove(collision.gameObject);
            }
        }
    } 
}
