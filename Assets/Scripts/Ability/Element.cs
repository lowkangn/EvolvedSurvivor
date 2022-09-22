using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
        public static readonly int maxLevel = 5;
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

        public StatusEffect GenerateEffect(ElementType type, float utilityRatio, float magnitude)
        {
            float levelRatio = (float)elements[type] / maxLevel;
            StatusEffect effect;
            switch (type)
            {
                case ElementType.Plasma:
                    effect = new PlasmaStatusEffect();
                    effect.Build(levelRatio, utilityRatio, magnitude);
                    break;
                case ElementType.Cryo:
                    effect = new CryoStatusEffect();
                    effect.Build(levelRatio, utilityRatio, magnitude);
                    break;
                case ElementType.Force:
                    effect = new PlasmaStatusEffect();
                    effect.Build(levelRatio, utilityRatio, magnitude);
                    break;
                case ElementType.Infect:
                    effect = new PlasmaStatusEffect();
                    effect.Build(levelRatio, utilityRatio, magnitude);
                    break;
                case ElementType.Pyro:
                    effect = new PlasmaStatusEffect();
                    effect.Build(levelRatio, utilityRatio, magnitude);
                    break;
                default:
                    throw new System.Exception("Element Type invalid");
            }
            return effect;
        }
    }
}
