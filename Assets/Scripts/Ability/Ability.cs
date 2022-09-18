using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections.Generic;

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
            Build(traitChart);
            hasBuilt = true;
        }

        /// <summary>
        /// Updates the ability using the consumed ability.
        /// </summary>
        public abstract void UpgradeAbility(Ability consumedAbility);

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

        protected abstract void Build(TraitChart traitChart);

        protected abstract void Activate();

        private void BuildElement()
        {
            element = new Element();

            int elementPoints = tier % 2 + tier / 2;

            for (int i = 0; i < elementPoints; i++)
            {
                ElementType chosenType = (ElementType)Random.Range(0, 5);
                element.elements[chosenType] += 1;
            }
        }
    }
}
