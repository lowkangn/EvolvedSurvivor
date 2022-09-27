using TeamOne.EvolvedSurvivor;
using UnityEngine;
using UnityEngine.UI;

// This class handles the player actions in the level up screen.
public class LevelUpScreenManager : MonoBehaviour
{
    private readonly string ERROR_MAX_ABILITIES_REACHED = "Current abilities are full!";

    [Header("UI elements")]
    [SerializeField] private Text textObj;
    [SerializeField] private GameObject addAbilityMenu;
    [SerializeField] private GameObject mergeAbilityMenu;

    [SerializeField] private CurrentAbilityUI[] currentAbilitiesButtons;

    private int nextAbilityIndex = 0;
    private int maxAbilityCount;

    private void Start()
    {
        maxAbilityCount = currentAbilitiesButtons.Length;
    }

    public void SwitchToAddAbilityMenu()
    {
        addAbilityMenu.SetActive(true);
        mergeAbilityMenu.SetActive(false);
    }

    public void SwitchToMergeMenu()
    {
        addAbilityMenu.SetActive(false);
        mergeAbilityMenu.SetActive(true);
    }

    public void AddNewAbility(Ability ability)
    {
        if (nextAbilityIndex >= maxAbilityCount)
        {
            textObj.text = ERROR_MAX_ABILITIES_REACHED;
        } 
        else
        {
            currentAbilitiesButtons[nextAbilityIndex].AddAbilityToButton(ability);
        }  
    }

    public void SaveSelection()
    {
        nextAbilityIndex++;
    }
}
