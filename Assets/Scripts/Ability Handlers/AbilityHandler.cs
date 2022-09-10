using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class AbilityHandler : MonoBehaviour
    {
        protected bool isStatsSet = false;

        protected IEnumerator kill()
        {
            yield return new WaitForSecondsRealtime(0.1f);
            gameObject.SetActive(false);
        }
    }
}
