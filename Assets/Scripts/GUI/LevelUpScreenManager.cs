using UnityEngine;

public class LevelUpScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject addAbilityMenu;
    [SerializeField] private GameObject mergeMenu;

    public void SwitchToAddAbilityMenu()
    {
        addAbilityMenu.SetActive(true);
        mergeMenu.SetActive(false);
    }

    public void SwitchToMergeMenu()
    {
        addAbilityMenu.SetActive(false);
        mergeMenu.SetActive(true);
    }
}
