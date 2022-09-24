using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class DamageReceiver : MonoBehaviour
    {
        private DamageHandler damageHandler;

        private void Start()
        {
            damageHandler = GetComponentInParent<DamageHandler>();
        }

        public void TakeDamage(Damage damage)
        {
            damageHandler.ProcessIncomingDamage(damage);
        }
    }
}
