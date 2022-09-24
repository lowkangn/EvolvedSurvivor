using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// A simple component meant to be added to the Add Ability button to show Add Ability screen
    /// </summary>
    [AddComponentMenu("TopDown Engine/GUI/MergeAbilityButton")]
    public class MergeAbilityButton : MonoBehaviour
	{
        public GameObject uiCamera;
        private ESGUIManager esGuiManager;

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
            esGuiManager = uiCamera.GetComponent<ESGUIManager>();
            esGuiManager.ShowMergeAbilitiesScreen();
            // we trigger a Pause event for the GameManager and other classes that could be listening to it too
            // TopDownEngineEvent.Trigger(TopDownEngineEventTypes.ShowMergeScreen, null);
        }
    }
}