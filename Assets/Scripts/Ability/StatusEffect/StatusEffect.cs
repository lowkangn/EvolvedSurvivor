using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class StatusEffect : MonoBehaviour
    {
        public abstract void Build(float levelRatio, float utilityRatio, float maxMagnitude);

        public abstract string GetName();

        public abstract void Apply(StatusEffectHandler handler, Damage damage);
    }
}
