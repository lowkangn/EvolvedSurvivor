using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class BasicEnemyDamage : MonoBehaviour
    {
        void Start()
        {
            float minDamageValue = 5.0f;
            float maxDamageValue = 10.0f;
            Damage damageObj = new Damage(Random.Range(minDamageValue, maxDamageValue), gameObject, new Vector3(0, 0, 0));

            DamageArea area = gameObject.GetComponent<DamageArea>();
            area.SetDamage(damageObj);
            area.SetActive(true);
        }
    }
}
