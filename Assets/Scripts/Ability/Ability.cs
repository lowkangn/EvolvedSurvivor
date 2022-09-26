using UnityEngine;
using MoreMountains.Tools;
using System.Collections.Generic;
using System.Linq;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class Ability : MonoBehaviour
    {
        [Header("Whether this ability will be activated while it is active")]
        [SerializeField]
        private bool activateOnlyOnce = false;
        [Header("The ability is activated once every (coolDown) seconds")]
        [SerializeField]
        protected AbilityStat<float> coolDown;
        protected int tier;
        protected TraitChart traitChart;
        protected Element element;
        [SerializeField]
        protected Damage projDamage = new Damage();
        [SerializeField]
        private bool hasBuilt = false;
        private bool hasActivated;
        private float coolDownTimer;
        public GameObject sprite;

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
        }

        /// <summary>
        /// Updates the ability using the consumed ability.
        /// </summary>
        public void UpgradeAbility(Ability consumedAbility)
        {
            // Element Upgrade
            tier += consumedAbility.tier;
            int additionalLevel = tier % 2 + tier / 2 - element.GetTotalLevel();
            if (additionalLevel > 0)
            {
                element.CombineWith(consumedAbility.element);
            }

            // Trait Chart Upgrade
            traitChart.CombineWith(consumedAbility.traitChart);

            // Build Ability Again
            Build();
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

            if (activateOnlyOnce)
            {
                if (!hasActivated)
                {
                    Activate();
                    hasActivated = true;
                }
                return;
            }

            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer < 0f)
            {
                Activate();
                coolDownTimer = coolDown.value;
            }
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

        protected abstract void Build();

        protected abstract void Activate();

        public GameObject GetSprite() {
            return this.sprite;
        }
    }
}
