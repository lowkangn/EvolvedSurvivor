using System.Collections;
using UnityEngine;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class CryoStatusEffect : StatusEffect
    {
        [SerializeField]
        private float duration;

        public override void Build(int tier, float utilityRatio, float maxMagnitude)
        {

        }

        public override void Apply(GameObject target)
        {
            target.GetComponent<Character>().Freeze();
            // TODO: Replace with new damageontouch script when avail
            //target.GetComponent<DamageOnTouch>().enabled = false;
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