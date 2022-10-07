using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class PassiveAbility : MonoBehaviour
    {
        protected const int NUM_OF_TIERS = 3;
        [SerializeField]
        protected int currentTier = 0;

        public abstract void Upgrade();

        public bool CanUpgrade()
        {
            return currentTier < NUM_OF_TIERS;
        }

    }
}
