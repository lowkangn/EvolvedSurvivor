using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class GameStartAbilityGenerator : MonoBehaviour
    {
        [SerializeField] private AbilityGenerator generator;
        [SerializeField] private GameObject abilityManager;

        void Start()
        {
            Ability newAbility = generator.GenerateAbility(1);
            AbilityManager manager = abilityManager.GetComponent<AbilityManager>();
            manager.AddAbility(newAbility);
        }
    }
}
