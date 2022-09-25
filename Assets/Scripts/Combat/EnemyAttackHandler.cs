using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class EnemyAttackHandler : MonoBehaviour
    {
        [SerializeField] private float damageValue;
        [SerializeField] private DamageHandler damageHandler;
        [SerializeField] private DamageArea damageArea;

        void Start()
        {
            Damage damage = new Damage();
            damage.damage = damageValue;

            damage = damageHandler.ProcessOutgoingDamage(damage);
            damageArea.SetDamage(damage);
            damageArea.SetActive(true);
        }
    }
}

