using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TeamOne.EvolvedSurvivor
{
    public class LockOnAbilityHandler : AbilityHandler
    {
        [SerializeField]
        private float timeOnScreen = 0.5f;
        void OnEnable()
        {
            StartCoroutine(wait());
        }

        IEnumerator wait()
        {
            yield return new WaitForSecondsRealtime(timeOnScreen);
            StartCoroutine(kill());
        }
    }
}