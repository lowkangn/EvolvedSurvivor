using UnityEngine;
using MoreMountains.Tools;
using System.Collections.Generic;
using System.Linq;

namespace TeamOne.EvolvedSurvivor
{
    public enum ElementType
    {
        Plasma = 0,
        Cryo = 1,
        Force = 2,
        Infect = 3,
        Pyro = 4
    }

    public class Element
    {
        public Dictionary<ElementType, int> elements;

        public Element()
        {
            elements = new Dictionary<ElementType, int>()
            {
                { ElementType.Plasma, 0 },
                { ElementType.Cryo, 0 },
                { ElementType.Force, 0 },
                { ElementType.Infect, 0 },
                { ElementType.Pyro, 0 }
            };
        }

        public int GetTotalLevel()
        {
            return elements.Sum(x => x.Value);
        }

        public void CombineWith(Element other)
        {
            for (int i = 0; i < 5; i++)
            {
                elements[(ElementType)i] += other.elements[(ElementType)i];
            }
        }
    }

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

        private bool hasBuilt = false;
        private bool hasActivated;
        private float coolDownTimer;

        protected GameObject playerRef;

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
                    hasActivated = Activate();
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

        public void addPlayerRef(GameObject player)
        {
            playerRef = player;
        }
        
        protected abstract void Build();

        protected abstract void Activate();
    }
}
