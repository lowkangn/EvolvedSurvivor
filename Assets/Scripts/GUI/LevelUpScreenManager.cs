using MoreMountains.Tools;
using System.Collections.Generic;
using TeamOne.EvolvedSurvivor;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

// This class handles the player actions in the level up screen.
public class LevelUpScreenManager : MonoBehaviour
{
    private const string ERROR_MAX_ABILITIES_REACHED = "Current abilities are full!";

    [Header("UI elements")]
    [SerializeField] private Text textObj;
    [SerializeField] private GameObject addAbilityMenu;
    [SerializeField] private GameObject mergeAbilityMenu;
    [SerializeField] private LevelUpHeaderButton addAbilityHeaderButton;
    [SerializeField] private LevelUpHeaderButton mergeAbilityHeaderButton;

    [SerializeField] private CurrentAbilityUI[] currentAbilities;

    [Header("Action handlers")]
    [SerializeField] private AddAbilityHandler addAbilityHandler;
    [SerializeField] private MergeAbilityHandler mergeAbilityHandler;

    private AbilityManager abilityManager;
    private int nextAbilityIndex = 0;
    private int maxAbilityCount;

    private void Awake()
    {
        this.maxAbilityCount = this.currentAbilities.Length;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        this.abilityManager = player.GetComponentInChildren<AbilityManager>();
    }

    private void OnEnable()
    {
        RefreshCurrentAbilities();

        if (this.nextAbilityIndex >= this.maxAbilityCount)
        {
            SwitchToMergeMenu();
        }
        else 
        {
            SwitchToAddAbilityMenu();
        }
    }

    public void SwitchToAddAbilityMenu()
    {
        foreach (CurrentAbilityUI currentAbility in currentAbilities)
        {
            currentAbility.DisableButton();
        }
        mergeAbilityHandler.ClearInput();

        this.addAbilityMenu.SetActive(true);
        this.mergeAbilityMenu.SetActive(false);

        this.addAbilityHeaderButton.SetSelected();
        this.mergeAbilityHeaderButton.ResetAppearance();
    }

    public void SwitchToMergeMenu()
    {
        foreach (CurrentAbilityUI currentAbility in currentAbilities)
        {
            currentAbility.EnableButton();
        }

        this.addAbilityHandler.ClearCurrentSelectedAbility();

        if (nextAbilityIndex < maxAbilityCount)
        {
            this.currentAbilities[nextAbilityIndex].RemoveAbility();
        }

        this.addAbilityMenu.SetActive(false);
        this.mergeAbilityMenu.SetActive(true);

        this.addAbilityHeaderButton.ResetAppearance();
        this.mergeAbilityHeaderButton.SetSelected();
    }

    public void AddNewAbility(Ability ability)
    {
        if (this.nextAbilityIndex >= this.maxAbilityCount)
        {
            this.textObj.text = ERROR_MAX_ABILITIES_REACHED;
        } 
        else
        {
            this.currentAbilities[nextAbilityIndex].AddAbilityToButton(ability);
        }  
    }

    private void RefreshCurrentAbilities()
    {
        foreach (CurrentAbilityUI currentAbility in currentAbilities)
        {
            currentAbility.RemoveAbility();
        }

        List<Ability> abilities = abilityManager.Abilities;

        for (int i = 0; i < abilities.Count; i++)
        {
            currentAbilities[i].AddAbilityToButton(abilities[i]);   
        }
        nextAbilityIndex = abilities.Count;
    }
}
