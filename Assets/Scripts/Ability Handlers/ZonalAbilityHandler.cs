using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class ZonalAbilityHandler : AbilityHandler
    {
        private float duration = 0;

        private void OnEnable()
        {
            StartCoroutine(WaitAndKillCoroutine());
        }

        public void SetDuration(float duration)
        {
            this.duration = duration;
        }

        IEnumerator WaitAndKillCoroutine()
        {
            yield return new WaitForSeconds(duration);
            StartCoroutine(KillCoroutine());
        }
    }
}