using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamOne.EvolvedSurvivor;

public class LevelUpSystem : MonoBehaviour
{
    [SerializeField] private MoreMountains.Tools.NewAbilityUI[] NewAbilitiesButtons;
    [SerializeField] private CurrentAbilityLevelUpUI[] CurrentAbilitiesButtons;
    private Ability currentSelectedAbility; 

    private Ability[] NewAbilities;

    private GameObject player;
    private AbilityManager abilityManager;
    private AbilityGenerator abilityGenerator;

    void OnEnable() {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
            abilityManager = player.GetComponentInChildren<AbilityManager>();
            abilityGenerator = player.GetComponentInChildren<AbilityGenerator>();
        }
        NewAbilities = new Ability[4];
        GetNewAbilities();
        GetCurrentAbilities();
    }

    private void OnDisable()
    {
        foreach (Ability ability in NewAbilities)
        {
            if (ability != null)
            {
                Destroy(ability.gameObject);
            }
        }
    }

    // Generate 4 new abilities in Level Up screen
    void GetNewAbilities() {
        for (int i = 0; i < 4; i++) {
            Ability ability = abilityGenerator.GenerateAbility(1);
            NewAbilitiesButtons[i].AddAbilityToButton(ability);
            NewAbilities[i] = ability;
        }
    }

    // Populate Current Abilities in Level Up screen
    void GetCurrentAbilities() {
        // Get player's current abilities
        List<Ability> Abilities = abilityManager.Abilities;
        int numOfCurrentAbilities = Abilities.Count;

        // Add ability to current ability buttons
        for (int i = 0; i < numOfCurrentAbilities; i++) {
            CurrentAbilitiesButtons[i].AddAbilityToCurrent(Abilities[i].sprite);
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
