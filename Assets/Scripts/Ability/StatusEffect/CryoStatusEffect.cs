using System.Collections;
using UnityEngine;
using MoreMountains.TopDownEngine;

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
            target.GetComponent<Character>().Freeze();
            // TODO: Replace with new damageontouch script when avail
            // target.GetComponent<DamageOnTouch>().enabled = false;
            StartCoroutine(FreezeFor(duration, target));
        }

        IEnumerator FreezeFor(float seconds, GameObject target)
        {
            yield return new WaitForSeconds(seconds);
            target.GetComponent<Character>().UnFreeze();
            //enemy.GetComponent<DamageOnTouch>().enabled = true;
        }
    }
}