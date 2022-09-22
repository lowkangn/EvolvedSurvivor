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
    }
}
