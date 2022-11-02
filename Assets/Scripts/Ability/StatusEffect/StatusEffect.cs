using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class StatusEffect : MonoBehaviour
    {
        public abstract void Build(int level, float magnitude);

        public abstract string GetName();

        public abstract void Apply(StatusEffectHandler handler, Damage damage);
    }
}
