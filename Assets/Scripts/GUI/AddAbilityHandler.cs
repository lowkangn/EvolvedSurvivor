using System.Collections.Generic;
using UnityEngine;
using TeamOne.EvolvedSurvivor;

public class AddAbilityHandler : MonoBehaviour
{
    [SerializeField] private NewAbilityOptionUI[] NewAbilitiesButtons;
    private Ability currentSelectedAbility;

    private Ability[] NewAbilities;

    private AbilityManager abilityManager;
    private AbilityGenerator abilityGenerator;
    private int numOfAbilityOptions;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        abilityManager = player.GetComponentInChildren<AbilityManager>();
        abilityGenerator = player.GetComponentInChildren<AbilityGenerator>();

        numOfAbilityOptions = NewAbilitiesButtons.Length;
        NewAbilities = new Ability[numOfAbilityOptions];
    }

    private void OnEnable() 
    {
        GenerateNewAbilities();
    }

    private void OnDisable() 
    {
        if (currentSelectedAbility != null)
        {
            SaveSelectedAbility();
        }

        ClearUnselectedAbilities();
    }

    // Generate new abilities in Level Up screen
    public void GenerateNewAbilities() 
    {
        for (int i = 0; i < numOfAbilityOptions; i++) {
            Ability ability = abilityGenerator.GenerateAbility(1);
            NewAbilitiesButtons[i].AddAbilityToButton(ability);
            NewAbilities[i] = ability;
        }
    }

    public void SetCurrentSelectedAbility(Ability ability) 
    {
        this.currentSelectedAbility = ability;
    }

    public void ClearCurrentSelectedAbility()
    {
        this.currentSelectedAbility = null;
    }

    // Save selected ability to player's current abilities
    public void SaveSelectedAbility() 
    {
        abilityManager.AddAbility(currentSelectedAbility);
    }

    private void ClearUnselectedAbilities()
    {
        foreach (Ability ability in NewAbilities)
        {
            if (currentSelectedAbility != null 
                && ability != currentSelectedAbility)
            {
                Destroy(ability.gameObject);
            }
        }

        NewAbilities = new Ability[numOfAbilityOptions];
    }
}
