using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    [System.Serializable]
    public class TraitChart
    {
        public static readonly float maxValue = 15;

        public float damage;
        public float DamageRatio => damage / maxValue;

        public float uptime;
        public float UptimeRatio => uptime / maxValue;

        public float aoe;
        public float AoeRatio => aoe / maxValue;

        public float quantity;
        public float QuantityRatio => quantity / maxValue;

        public float utility;
        public float UtilityRatio => utility / maxValue;
    }
}
