using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class CryoStatusEffect : StatusEffect
    {
        [SerializeField]
        private float duration;

        public override void Build(float levelRatio, float utilityRatio, float maxMagnitude)
        {
            duration = levelRatio * utilityRatio * maxMagnitude;
        }

        public override void Apply(GameObject target, Damage damage)
        {
            target.GetComponent<Enemy>().FreezeForDuration(duration);
        }

        public override string GetName()
        {
            return "Cryo";
        }
    }
}