using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class GameStartAbilityGenerator : MonoBehaviour
    {
        [SerializeField] private AbilityGenerator generator;
        [SerializeField] private GameObject abilityManager;
        [SerializeField] private int startLevel = 1;

        void Start()
        {
            Ability newAbility = generator.GenerateAbility(startLevel);
            AbilityManager manager = abilityManager.GetComponent<AbilityManager>();
            manager.AddAbility(newAbility);
        }
    }
}
