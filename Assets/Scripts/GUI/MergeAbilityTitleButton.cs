using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// A simple component meant to be added to the Merge Ability button to show Merge Ability screen
    /// </summary>
    [AddComponentMenu("TopDown Engine/GUI/MergeAbilityTitleButton")]
    public class MergeAbilityTitleButton : MonoBehaviour
	{
        [SerializeField] private ESGUIManager esGuiManager;

        /// <summary>
        /// Show merge abilities screen event
        /// </summary>
	    public virtual void ShowMergeScreenAction()
	    {
            StartCoroutine(ShowMergeScreenCo());
        }	

        /// <summary>
        /// A coroutine used to trigger the event to show merge abilities screen
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator ShowMergeScreenCo()
        {
            yield return null;
            esGuiManager.ShowMergeAbilitiesScreen();
        }
    }
}