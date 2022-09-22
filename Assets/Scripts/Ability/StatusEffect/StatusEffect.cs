using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class StatusEffect : MonoBehaviour
    {
        public abstract void Build(float levelRatio, float utilityRatio, float maxMagnitude);
        public abstract void Apply(GameObject enemy, Damage damage);
    }
}
