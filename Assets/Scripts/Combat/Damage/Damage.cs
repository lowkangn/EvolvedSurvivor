using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    [System.Serializable]
    public class Damage
    {
        public float damage;
        public GameObject instigator;
        public Vector3 direction;
        public List<StatusEffect> effects;

        public Damage(float damage, GameObject instigator)
        {
            this.damage = damage;
            this.instigator = instigator;
            this.direction = Vector3.zero;
            this.effects = new List<StatusEffect>();
        }   
    }
}
