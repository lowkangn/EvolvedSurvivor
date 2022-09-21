using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class StatusEffect : MonoBehaviour
    {
        public abstract void Build(int tier, float utilityRatio, float maxMagnitude);
        public abstract void Apply(GameObject enemy);
    }
}
