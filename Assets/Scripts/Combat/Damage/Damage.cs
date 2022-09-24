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
        public Vector3 direction = Vector3.zero;
        public List<StatusEffect> effects = new List<StatusEffect>();

        public Damage()
        {

        }

        public Damage(float damage, GameObject instigator)
        {
            this.damage = damage;
            this.instigator = instigator;
        }

        public Damage(float damage, GameObject instigator, Vector3 direction)
        {
            this.damage = damage;
            this.instigator = instigator;
            this.direction = direction;
        }
         
    }
}
