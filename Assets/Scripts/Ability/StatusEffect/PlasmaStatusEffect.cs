using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class PlasmaStatusEffect : StatusEffect
    {
        private readonly float radius = 0.8f;
        private float damageMultiplier;
        private int level;
        public override void Build(int level, float magnitude)
        {
            this.level = level;
            damageMultiplier = magnitude;
        }

        public override void Apply(StatusEffectHandler handler, Damage damage)
        {
            if (damageMultiplier > 0)
            {
                Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(handler.gameObject.transform.position, radius, LayerMask.GetMask("Enemies"));
                if (enemiesInRadius.Length > 0)
                {
                    float nearestDist = -1f;
                    GameObject nearest = null;

                    foreach (Collider2D currCollider in enemiesInRadius)
                    {
                        if (currCollider.gameObject.tag == "Enemy")
                        {
                            Vector3 currDirection = currCollider.GetComponent<Transform>().position - handler.gameObject.transform.position;
                            float dist = currDirection.magnitude;
                            if (nearestDist == -1f || dist < nearestDist)
                            {
                                nearestDist = dist;
                                nearest = currCollider.gameObject;
                            }
                        }
                    }
                    if (nearest != null)
                    {
                        handler.ApplyChaining(new Damage(damage.damage * damageMultiplier, gameObject), nearest);
                    }
                }
            }
        }

        public override string GetName()
        {
            return "Plasma " + level;
        }
    }
}
