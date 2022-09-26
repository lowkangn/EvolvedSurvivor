using MoreMountains.Tools;
using UnityEngine;

// This class handles the UI changes for the level up screen.
public class LevelUpScreenManager : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField] private GameObject addAbilityMenu;
    [SerializeField] private GameObject mergeAbilityMenu;
    [SerializeField] private MMTouchButton addAbilityHeaderButton;
    [SerializeField] private MMTouchButton mergeAbilityHeaderButton;

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
