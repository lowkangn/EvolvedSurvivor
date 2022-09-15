using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// A simple component meant to be added to the Add Ability button to show Add Ability screen
    /// </summary>
    [AddComponentMenu("TopDown Engine/GUI/AddAbilityButton")]
    public class AddAbilityButton : MonoBehaviour
	{
        public GameObject confirmButton;

		/// <summary>
        /// Show add abilities screen event
        /// </summary>
	    public virtual void ShowAddScreenAction()
	    {
            // Remove confirmButton until an ability is pressed
            confirmButton.SetActive(false);
            StartCoroutine(ShowAddScreenCo());
        }	

        /// <summary>
        /// A coroutine used to trigger the event to show add abilities screen
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator ShowAddScreenCo()
        {
            yield return null;
            // we trigger a Pause event for the GameManager and other classes that could be listening to it too
            TopDownEngineEvent.Trigger(TopDownEngineEventTypes.ShowAddAbilityScreen, null);
        }

    }
}