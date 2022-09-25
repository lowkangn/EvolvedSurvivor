using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.EventSystems;
using MoreMountains.TopDownEngine;

/// <summary>
/// Handles all GUI effects and changes not handled by GUIManager.
/// </summary>
public class ESGUIManager : MonoBehaviour, MMEventListener<TopDownEngineEvent>
{
    [SerializeField] private GUIUpdaterScriptableObject guiUpdater;
    [SerializeField] private GameManager gameManager;

    /// the XP bars to update
    [Tooltip("the XP bars to update")]
    public MMProgressBar[] XPBars;

    /// the level up screen game object
    [Tooltip("the level up screen game object")]
    public GameObject LevelUpScreen;
    /// the Merge Abilities screen game object
    [Tooltip("the Merge Abilities screen game object")]
    public GameObject MergeAbilitiesScreen;

    private bool isLevelUpScreenVisible = false;
    private bool isMergeAbilitiesScreenVisible = false;

    private void OnEnable()
    {
        guiUpdater.XpChangeEvent.AddListener(UpdateXPBar);
        guiUpdater.LevelUpEvent.AddListener(ShowLevelUpScreen);
        this.MMEventStartListening<TopDownEngineEvent>();
    }

    private void OnDisable()
    {
        guiUpdater.XpChangeEvent.RemoveListener(UpdateXPBar);
        guiUpdater.LevelUpEvent.RemoveListener(ShowLevelUpScreen);
        this.MMEventStopListening<TopDownEngineEvent>();
    }

    /// <summary>
    /// Shows the level up screen.
    /// </summary>
    public void ShowLevelUpScreen()
    {
        if (LevelUpScreen != null)
        {
            if (isMergeAbilitiesScreenVisible) {
                MergeAbilitiesScreen.SetActive(false);
                isMergeAbilitiesScreenVisible = false;
            }
            isLevelUpScreenVisible = true;
            LevelUpScreen.SetActive(true);
            EventSystem.current.sendNavigationEvents = true;
            
            // if time is not already stopped		
			if (Time.timeScale>0.0f){
                gameManager.Pause(PauseMethods.NoPauseMenu);
            }
        }
    }

    /// <summary>
    /// Updates the XP bar.
    /// </summary>
    /// <param name="currentXP">Current XP.</param>
    /// <param name="maxXP">Max XP.</param>
    public void UpdateXPBar(float currentXP, float maxXP)
    {
        if (XPBars == null) { return; }
        if (XPBars.Length <= 0) { return; }

        foreach (MMProgressBar XPBar in XPBars)
        {
            if (XPBar == null) { continue; }
            XPBar.UpdateBar(currentXP, 0f, maxXP);
        }
    }

    public void OnMMEvent(TopDownEngineEvent eventType)
    {
        bool isTogglePauseEvent = eventType.EventType == TopDownEngineEventTypes.TogglePause;

        if (isTogglePauseEvent && isLevelUpScreenVisible) {
            isLevelUpScreenVisible = false;
            LevelUpScreen.SetActive(false);
        } else if (isTogglePauseEvent && isMergeAbilitiesScreenVisible) {
            isMergeAbilitiesScreenVisible = false;
            MergeAbilitiesScreen.SetActive(false);
        }
    }

    /// <summary>
    /// Shows the MergeAbilities screen.
    /// </summary>
    public void ShowMergeAbilitiesScreen()
    {
        if (MergeAbilitiesScreen != null)
        {
            if (isLevelUpScreenVisible) {
                LevelUpScreen.SetActive(false);
                isLevelUpScreenVisible = false;
            }
            isMergeAbilitiesScreenVisible = true;
            MergeAbilitiesScreen.SetActive(true);
            EventSystem.current.sendNavigationEvents = true;

            // if time is not already stopped		
			if (Time.timeScale>0.0f){
                gameManager.Pause(PauseMethods.NoPauseMenu);
            }
        }
    }
}
