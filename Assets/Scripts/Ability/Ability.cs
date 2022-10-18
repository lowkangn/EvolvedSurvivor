using UnityEngine;
using System.Collections.Generic;
using MoreMountains.Tools;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class Ability : MonoBehaviour, Upgradable
    {
        public string AbilityName => abilityName;
        [SerializeField] private string abilityName;

        [Header("Whether this ability will be activated while it is active")]
        [SerializeField]
        private bool activateOnlyOnce = false;

        [Header("The ability is activated once every (coolDown) seconds")]
        [SerializeField]
        protected AbilityStat<float> coolDown;

        protected int tier;
        private readonly int maxTier = 10;
        protected TraitChart traitChart;
        protected Element element;
        protected List<StatusEffect> effects = new();
        [SerializeField]
        private bool hasBuilt = false;
        private bool hasActivated;
        private float coolDownTimer;
        [SerializeField]
        private Sprite abilitySprite;
        private bool isActive;

        [Header("Projectile pool")]
        [SerializeField]
        protected MMObjectPooler objectPool;

        [Header("Element Magnitudes")]
        [SerializeField]
        private float maxPlasmaMagnitude;
        [SerializeField]
        private float maxCryoMagnitude;
        [SerializeField]
        private float maxForceMagnitude;
        [SerializeField]
        private float maxInfectMagnitude;
        [SerializeField]
        private float maxPyroMagnitude;
        protected Dictionary<ElementType, float> elementMagnitudes =  new Dictionary<ElementType, float>();

        protected DamageHandler damageHandler;
        private AbilityGenerator abilityGenerator;

        protected float coolDownMultiplier = 1f;

        /// <summary>
        /// Uses the trait chart to define the behaviours of the ability. 
        /// E.g., Speed, Damage, CoolDown, etc.
        /// MUST be called before the ability can be activated
        /// </summary>
        public void BuildAbility(int tier, TraitChart traitChart)
        {
            this.tier = tier;
            this.traitChart = traitChart;
            BuildElement();
            Build();
            hasBuilt = true;
            isActive = true;
        }

        public void SetOwner(Transform owner, AbilityGenerator abilityGenerator)
        {
            damageHandler = owner.GetComponentInParent<DamageHandler>();
            this.abilityGenerator = abilityGenerator;
        }

        private void CopyAbility(Ability other)
        {
            tier = other.tier;
            traitChart = new TraitChart(other.traitChart);
            element = other.element;
        }

        /// <summary>
        /// Creates a new ability based on this ability and the consumed ability
        /// </summary>
        public Ability UpgradeAbility(Ability consumedAbility)
        {
            if (CanUpgrade(consumedAbility))
            {
                Ability newAbility = Instantiate(abilityGenerator.GetPrefab(abilityName));
                newAbility.CopyAbility(this);
                // Element Upgrade
                newAbility.tier = tier + consumedAbility.tier;
                int additionalLevel = tier % 2 + tier / 2 - element.GetTotalLevel();
                if (additionalLevel > 0)
                {
                    newAbility.element.CombineWith(consumedAbility.element);
                }

                // Trait Chart Upgrade
                newAbility.traitChart.CombineWith(consumedAbility.traitChart);

                // Build Ability Again
                newAbility.Build();
                newAbility.hasBuilt = true;
                newAbility.isActive = true;
                return newAbility;
            } 
            else
            {
                throw new System.Exception("Trying to upgrade past max tier");
            }
        }

        /// <summary>
        /// Checks that the ability can be upgraded if combined tier does not exceed max.
        /// </summary>
        public bool CanUpgrade(Ability consumedAbility)
        {
            return (tier + consumedAbility.tier <= maxTier);
        }


        private void Awake()
        {
            elementMagnitudes.Add(ElementType.Plasma, maxPlasmaMagnitude);
            elementMagnitudes.Add(ElementType.Cryo, maxCryoMagnitude);
            elementMagnitudes.Add(ElementType.Force, maxForceMagnitude);
            elementMagnitudes.Add(ElementType.Infect, maxInfectMagnitude);
            elementMagnitudes.Add(ElementType.Pyro, maxPyroMagnitude);
        }

        private void Update()
        {
            if (!hasBuilt)
            {
                return;
            }

            if (activateOnlyOnce && isActive)
            {
                if (!hasActivated)
                {
                    Activate();
                    hasActivated = true;
                }
                return;
            }

            if (isActive && damageHandler != null)
            {
                coolDownTimer -= Time.deltaTime;
                if (coolDownTimer < 0f)
                {
                    Activate();
                    coolDownTimer = coolDown.value * this.coolDownMultiplier;
                }
            }
        }

        private void OnDestroy()
        {
            objectPool.DestroyObjectPool();
        }

        private void BuildElement()
        {
            element = new Element();

            int elementLevel = tier % 2 + tier / 2;

            for (int i = 0; i < elementLevel; i++)
            {
                ElementType chosenType = (ElementType)Random.Range(0, 5);
                element.elements[chosenType] += 1;
            }
        }
        protected StatusEffect GenerateEffect(ElementType type, float utilityRatio, float magnitude)
        {
            float levelRatio = (float)element.elements[type] / Element.maxLevel;
            StatusEffect effect;
            switch (type)
            {
                case ElementType.Plasma:
                    effect = gameObject.AddComponent<PlasmaStatusEffect>();
                    effect.Build(levelRatio, utilityRatio, magnitude);
                    break;
                case ElementType.Cryo:
                    effect = gameObject.AddComponent<CryoStatusEffect>();
                    effect.Build(levelRatio, utilityRatio, magnitude);
                    break;
                case ElementType.Force:
                    effect = gameObject.AddComponent<ForceStatusEffect>();
                    effect.Build(levelRatio, utilityRatio, magnitude);
                    break;
                case ElementType.Infect:
                    effect = gameObject.AddComponent<InfectStatusEffect>();
                    effect.Build(levelRatio, utilityRatio, magnitude);
                    break;
                case ElementType.Pyro:
                    effect = gameObject.AddComponent<PyroStatusEffect>();
                    effect.Build(levelRatio, utilityRatio, magnitude);
                    break;
                default:
                    throw new System.Exception("Element Type invalid");
            }
            return effect;
        }

        public Sprite GetSprite()
        {
            return this.abilitySprite;
        }

        protected abstract void Build();

        protected abstract void Activate();

        public void Stop()
        {
            isActive = false;
        }

        public string GetAbilityName()
        {
            return $"Level {tier} {abilityName}\n";
        }

        public string GetDescription()
        {
            return $"Level {tier} {abilityName}\n" + traitChart.GetStatsDescription();
        }

        public void SetCoolDownMultiplier(float multiplier)
        {
            this.coolDownMultiplier = multiplier;
        }

        public bool IsAbility()
        {
            return true;
        }

        public bool IsPassiveAbility()
        {
            return false;
        }

        public string GetDamageDescription()
        {
            return traitChart.GetDamageDescription();
        }

        public string GetUptimeDescription()
        {
            return traitChart.GetUptimeDescription();
        }

        public string GetAoeDescription()
        {
            return traitChart.GetAoeDescription();
        }

        public string GetQuantityDescription()
        {
            return traitChart.GetQuantityDescription();
        } 

        public string GetUtilityDescription()
        {
            return traitChart.GetUtilityDescription();
        } 

        public TraitChart GetTraitChart()
        {
            return traitChart;
        }
    }
}
