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

    private void OnEnable() {
        GenerateNewAbilities();
    }

    private void OnDisable() {
        foreach (Ability ability in NewAbilities)
        {
            if (ability != null)
            {
                Destroy(ability.gameObject);
            }
        }
    }

    // Generate new abilities in Level Up screen
    public void GenerateNewAbilities() {
        for (int i = 0; i < numOfAbilityOptions; i++) {
            Ability ability = abilityGenerator.GenerateAbility(1);
            NewAbilitiesButtons[i].AddAbilityToButton(ability);
            NewAbilities[i] = ability;
        }
    }

    // Populate Current Abilities in Level Up screen
    public void GetCurrentAbilities() {
        // Get player's current abilities
        List<Ability> Abilities = abilityManager.Abilities;
        int numOfCurrentAbilities = Abilities.Count;

        // Add ability to current ability buttons
        for (int i = 0; i < numOfCurrentAbilities; i++) {
            //CurrentAbilitiesButtons[i].AddAbilityToCurrent(Abilities[i].sprite);
        }
    }

    public void SetCurrentSelectedAbility(Ability ability) {
        this.currentSelectedAbility = ability;
    }

    // Save current ability chosen to player's current abilities
    public void SaveCurrentAbilities() {
        abilityManager.AddAbility(currentSelectedAbility);

        foreach (Ability ability in NewAbilities) {
            if (ability != currentSelectedAbility) {
                Destroy(ability.gameObject);
            }
        }
        NewAbilities = new Ability[4];
    }
}
