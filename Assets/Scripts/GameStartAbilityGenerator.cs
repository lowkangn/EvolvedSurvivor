using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class GameStartAbilityGenerator : MonoBehaviour
    {
        [SerializeField] private BasicProjectileAbility basicProj;
        [SerializeField] private GameObject abilityManager;

        void Start()
        {
            BasicProjectileAbility basicProjObj = Instantiate(basicProj, new Vector3(0, 0, 0), Quaternion.identity);
            AbilityManager manager = abilityManager.GetComponent<AbilityManager>();
            manager.AddAbility(basicProjObj);
            basicProjObj.BuildAbility(new TraitChart());
        }
    }
}
