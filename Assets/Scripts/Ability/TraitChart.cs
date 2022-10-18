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

        public TraitChart()
        {
            this.damage = 0f;
            this.uptime = 0f;
            this.aoe = 0f;
            this.quantity = 0f;
            this.utility = 0f;
        }

        public TraitChart(float damage, float uptime, float aoe, float quantity, float utility)
        {
            this.damage = damage;
            this.uptime = uptime;
            this.aoe = aoe;
            this.quantity = quantity;
            this.utility = utility;
        }

        public TraitChart(TraitChart other)
        {
            this.damage = other.damage;           
            this.uptime = other.uptime;
            this.aoe = other.aoe;
            this.quantity = other.quantity;
            this.utility = other.utility;
        }

        public void CombineWith(TraitChart other)
        {
            damage = Mathf.Clamp(damage + other.damage, 0, maxValue);
            uptime = Mathf.Clamp(uptime + other.uptime, 0, maxValue);
            aoe = Mathf.Clamp(aoe + other.aoe, 0, maxValue);
            quantity = Mathf.Clamp(quantity + other.quantity, 0, maxValue);
            utility = Mathf.Clamp(utility + other.utility, 0, maxValue);
        }

        public float GetTotalPoints()
        {
            return damage + uptime + aoe + quantity + utility;
        }
        
        public string GetStatsDescription()
        {
            return $"Damage: {damage:0.0}\nUptime: {uptime:0.0}\nAOE: {aoe:0.0}\nQuantity: {quantity:0.0}\nUtility: {utility:0.0}\n";
        }

        public string GetDamageDescription()
        {
            return $"Damage: {damage:0.0}";
        }

        public string GetUptimeDescription()
        {
            return $"Uptime:\n{uptime:0.0}";
        }

        public string GetAoeDescription()
        {
            return $"AOE:\n{aoe:0.0}";
        }

        public string GetQuantityDescription()
        {
            return $"Quantity:\n{quantity:0.0}";
        } 

        public string GetUtilityDescription()
        {
            return $"Utility:\n{utility:0.0}";
        } 
    }
}
