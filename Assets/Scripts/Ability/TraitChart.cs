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

        public void CombineWith(TraitChart other)
        {
            damage = Mathf.Clamp(damage + other.damage, 0, maxValue);
            uptime = Mathf.Clamp(uptime + other.uptime, 0, maxValue);
            aoe = Mathf.Clamp(aoe + other.aoe, 0, maxValue);
            quantity = Mathf.Clamp(quantity + other.quantity, 0, maxValue);
            utility = Mathf.Clamp(utility + other.utility, 0, maxValue);
        }
    }
}
