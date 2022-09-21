using System.Collections;
using UnityEngine;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class CryoStatusEffect : StatusEffect
    {
        [SerializeField]
        private float duration;

        public CryoStatusEffect(float duration)
        {
            this.duration = duration;
        }

        public override void Apply(GameObject enemy)
        {
            enemy.GetComponent<Character>().Freeze();
            // TODO: Replace with new damageontouch script when avail
            enemy.GetComponent<DamageOnTouch>().enabled = false;
            StartCoroutine(FreezeFor(duration, enemy));
        }

        IEnumerator FreezeFor(float seconds, GameObject enemy)
        {
            yield return new WaitForSeconds(seconds);
            enemy.GetComponent<Character>().UnFreeze();
            enemy.GetComponent<DamageOnTouch>().enabled = true;
        }
    }
}