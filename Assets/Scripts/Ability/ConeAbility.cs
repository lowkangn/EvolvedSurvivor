using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class ConeAbility : Ability
    {
        [SerializeField]
        private AbilityStat<float> damage;
        [SerializeField]
        private AbilityStat<float> duration;
        [SerializeField]
        private AbilityStat<float> aoeRange;
        [SerializeField]
        private AbilityStat<int> coneNumber;

        float anglePerHalfCone = 15f;
        Vector2[] vertices;

        protected override void Activate()
        {
            GameObject projectile = objectPool.GetPooledGameObject();
            projectile.transform.parent = transform;
            projectile.transform.localPosition = Vector3.zero;
            projectile.transform.localScale = Vector3.one * aoeRange.value;

            ConeAbilityHandler handler = projectile.GetComponent<ConeAbilityHandler>();
            handler.UpdateParticles(aoeRange.value, coneNumber.value, anglePerHalfCone);

            PolygonCollider2D collider = projectile.GetComponent<PolygonCollider2D>();
            collider.SetPath(0, vertices);

            Damage damageObj = new Damage(damage.value, gameObject, effects);
            damageObj = damageHandler.ProcessOutgoingDamage(damageObj);

            DamageArea damageArea = projectile.GetComponent<DamageArea>();
            damageArea.SetDamage(damageObj);
            damageArea.SetLifeTime(duration.value);
            damageArea.SetActive(true);
        }

        private Vector2[] CalculateVertices()
        {
            Vector2[] vertices = new Vector2[2 + coneNumber.value * 2];
            vertices[0] = Vector2.zero;
            vertices[1 + coneNumber.value] = new Vector2(0, 1);
            for (int i = 0; i < coneNumber.value; i++)
            {
                float y = Mathf.Cos(Mathf.Deg2Rad * anglePerHalfCone * (i + 1));
                float x = Mathf.Sin(Mathf.Deg2Rad * anglePerHalfCone * (i + 1));
                vertices[i+1] = new Vector2(x * -1, y);
                vertices[^(i+1)] = new Vector2(x, y);
            }
            return vertices;
        }

        protected override void Build()
        {
            // Damage
            damage.value = (damage.maxValue - damage.minValue) * traitChart.DamageRatio + damage.minValue;

            // Uptime
            coolDown.value = coolDown.maxValue - (coolDown.maxValue - coolDown.minValue) * traitChart.UptimeRatio;
            duration.value = (duration.maxValue - duration.minValue) * traitChart.UptimeRatio + duration.minValue;

            // AOE
            aoeRange.value = (aoeRange.maxValue - aoeRange.minValue) * traitChart.AoeRatio + aoeRange.minValue;

            // Quantity
            coneNumber.value = Mathf.FloorToInt((coneNumber.maxValue - coneNumber.minValue) * traitChart.QuantityRatio + coneNumber.minValue);

            // Utility
            foreach (KeyValuePair<ElementType, int> el in element.elements)
            {
                if (el.Value > 0)
                {
                    effects.Add(GenerateEffect(el.Key, traitChart.UtilityRatio, elementMagnitudes[el.Key]));
                }
            }

            vertices = CalculateVertices();
        }
    }
}
