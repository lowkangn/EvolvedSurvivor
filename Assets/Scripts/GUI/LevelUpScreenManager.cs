using MoreMountains.Tools;
using TeamOne.EvolvedSurvivor;
using UnityEngine;

// This class handles the player actions in the level up screen.
public class LevelUpScreenManager : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField] private GameObject addAbilityMenu;
    [SerializeField] private GameObject mergeAbilityMenu;

    [SerializeField] private CurrentAbilityUI[] currentAbilitiesButtons;

    [Header("Action handlers")]
    [SerializeField] private AddAbilityHandler addAbilityHandler;
    [SerializeField] private MergeAbilityHandler mergeAbilityHandler;

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
}
