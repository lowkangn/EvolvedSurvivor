using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    [System.Serializable]
    public class AbilityStat<T>
    {
        public T minValue;
        public T maxValue;

        // [HideInInspector]
        public T value;
    }
}
