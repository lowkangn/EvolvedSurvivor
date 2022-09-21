using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TeamOne.EvolvedSurvivor
{
    public class LockOnAbilityHandler : AbilityHandler
    {
        float timeOnScreen = 0.5f;
        private void OnEnable()
        {
            StartCoroutine(WaitAndKillCoroutine());
        }

        IEnumerator WaitAndKillCoroutine()
        {
            yield return new WaitForSeconds(timeOnScreen);
            StartCoroutine(KillCoroutine());
        }
    }
}