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

        private float anglePerHalfCone = 15f;
        private ConeAbilityHandler cone;
        Vector2[] vertices;

        private float recursiveTimer = 0f;
        private readonly float recursiveTick = 3f;
        

        protected override void Update()
        {
            base.Update();

            if (cone != null)
            {
                cone.transform.position = this.transform.position;

                if (hasRecursive)
                {
                    if (recursiveTimer <= 0f)
                    {
                        RecursableDamageArea damageArea = cone.gameObject.GetComponent<RecursableDamageArea>();
                        Ability recursiveAbility = recursiveAbilityObjectPool.GetPooledGameObject().GetComponent<Ability>();
                        recursiveAbility.gameObject.SetActive(true);
                        damageArea.AddRecursiveAbility(recursiveAbility);

                        recursiveTimer = recursiveTick;
                    }
                    else
                    {
                        recursiveTimer -= Time.deltaTime;
                    }
                }
            }
        }

        private void OnDisable()
        {
            cone = null;
        }

        protected override void Activate()
        {
            GameObject projectile = projectileObjectPool.GetPooledGameObject();
            projectile.transform.position = this.transform.position;
            projectile.transform.localScale = Vector3.one * aoeRange.value;

            cone = projectile.GetComponent<ConeAbilityHandler>();
            cone.UpdateParticles(aoeRange.value, coneNumber.value, anglePerHalfCone);

            PolygonCollider2D collider = projectile.GetComponent<PolygonCollider2D>();
            collider.SetPath(0, vertices);

            Damage damageObj = new Damage(damage.value, gameObject, effects);
            damageObj = damageHandler.ProcessOutgoingDamage(damageObj);

            RecursableDamageArea damageArea = projectile.GetComponent<RecursableDamageArea>();
            damageArea.SetDamage(damageObj);
            damageArea.SetLifeTime(duration.value);

            // Add recursive ability if it is recursive
            if (hasRecursive)
            {
                Ability recursiveAbility = recursiveAbilityObjectPool.GetPooledGameObject().GetComponent<Ability>();
                recursiveAbility.gameObject.SetActive(true);
                damageArea.AddRecursiveAbility(recursiveAbility);
            }

            damageArea.SetActive(true);
            sfxHandler.PlaySfx();
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
                vertices[coneNumber.value - i] = new Vector2(-x, y);
                vertices[(coneNumber.value + 2 + i)] = new Vector2(x, y);
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
                    effects.Add(GenerateEffect(el.Key, traitChart.UtilityRatio, elementMagnitudes[(int)el.Key]));
                }
            }

            vertices = CalculateVertices();
        }

        protected override float DebuffTraitsForMerging(Ability other)
        {
            if (GetType() == other.GetType())
            {
                return 0f;
            }
            float points = other.traitChart.damage * debuffFactor;
            other.traitChart.damage -= points;
            return points;
        }

        protected override TraitChart CreateTraitChartForMerging(float pointsToAssign, bool isSameType)
        {            
            float damageRatio = traitChart.damage;
            float uptimeRatio = traitChart.uptime;
            float aoeRatio = traitChart.aoe;
            float quantityRatio = traitChart.quantity;
            float utilityRatio = traitChart.utility;
            if (!isSameType)
            {
                damageRatio = 0f;
            }
            pointsToAssign += traitChart.GetTotalPoints();
            float uptimeBuff = pointsToAssign * buffFactor;
            pointsToAssign -= uptimeBuff;
            float sum = damageRatio + uptimeRatio + aoeRatio + quantityRatio + utilityRatio;
            return new TraitChart(damageRatio / sum * pointsToAssign,
                uptimeRatio / sum * pointsToAssign + uptimeBuff,
                aoeRatio / sum * pointsToAssign,
                quantityRatio / sum * pointsToAssign,
                utilityRatio / sum * pointsToAssign);
        }

        protected override void HandleRecursive()
        {
            if (!hasActivated)
            {
                Activate();
                hasActivated = true;
                cone.SetRotating(true);
                Invoke("Deactivate", duration.value);
            }
        }

        protected override void Deactivate()
        {
            cone.SetRotating(false);
            base.Deactivate();
        }

        public override string GetDetails()
        {
            return $"{damage.value:0.0} damage every 0.5 seconds\n"
                + $"Fires every {coolDown.value:0.0} seconds\n"
                + $"Lasts {duration.value:0.0} seconds\n"
                + $"Range: {aoeRange.value:0.0} units\n"
                + $"Cone Angle: {(coneNumber.value * anglePerHalfCone * 2):0.0} degrees\n"
                + "\n"
                + GetStatusEffects();
        }

        public override string GetComparedDetails(Ability other)
        {
            ConeAbility o = (ConeAbility)other;

            string details = "";
            details += GetComparedFloatString(o.damage.value, damage.value) + " damage every 0.5 seconds\n";
            details += "Fires every " + GetComparedFloatString(o.coolDown.value, coolDown.value) + " seconds\n";
            details += "Lasts " + GetComparedFloatString(o.duration.value, duration.value) + " seconds\n";
            details += "Range: " + GetComparedFloatString(o.aoeRange.value, aoeRange.value) + " units\n";
            details += "Cone Angle: " + GetComparedIntString(o.coneNumber.value, coneNumber.value) + " degrees\n";
            details += "\n" + GetStatusEffects();

            return details;
        }
    }
}
