using System.Collections;
using System.Collections.Generic;
using TeamOne.EvolvedSurvivor;
using Unity.VisualScripting;
using UnityEngine;

public class MergeSystem : MonoBehaviour
{
    // The UI would call this class to get the result of merging pri & sec ability
    [SerializeField]
    public PriSecSlotUI primaryAbilitySlot;
    [SerializeField]
    public PriSecSlotUI secondaryAbilitySlot;
    [SerializeField]
    public MergeOutputSlotUI outputSlot;
    [SerializeField] 
    private CurrentAbilityMergeUI[] CurrentAbilitiesButtons;

    private GameObject player;
    private AbilityManager abilityManager;
    void OnEnable()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            abilityManager = player.GetComponentInChildren<AbilityManager>();
        }
        GetCurrentAbilities();
    }
    void GetCurrentAbilities()
    {
        // Get player's current abilities
        List<Ability> Abilities = abilityManager.Abilities;
        int numOfCurrentAbilities = Abilities.Count;

        // Add ability to current ability buttons
        /*for (int i = 0; i < numOfCurrentAbilities; i++)
        {
            CurrentAbilitiesButtons[i].AddAbility(Abilities[i]);
        }*/
    }

    // output ability & prefab (big box) if valid pri & sec
    public void UpdateOutput()
    {
        Ability primary = primaryAbilitySlot.GetAbility();
        Ability secondary = secondaryAbilitySlot.GetAbility();
        if (!primary.IsUnityNull() && !secondary.IsUnityNull() && primary.CanUpgrade(secondary))
        {
            Ability outputAbility = primary.UpgradeAbility(secondary);
            outputSlot.AddAbility(outputAbility);
        } 
        else
        {
            outputSlot.ClearSlot();
        }
    }

    public void MergeAbilities()
    {
        if (!outputSlot.IsEmpty())
        {
            Ability primary = primaryAbilitySlot.GetAbility();
            Ability secondary = secondaryAbilitySlot.GetAbility();
            Ability newAbility = outputSlot.GetAbility();
            abilityManager.RemoveAbility(primary);
            abilityManager.RemoveAbility(secondary);
            abilityManager.AddAbility(newAbility);
        }
    }
}
