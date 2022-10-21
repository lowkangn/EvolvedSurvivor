using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

namespace MoreMountains.TopDownEngine
{
	/// <summary>
	/// This class manages the XP of an object and XP bar
	/// </summary>
	[AddComponentMenu("TopDown Engine/Character/Core/XP")] 
	public class XP : MonoBehaviour
	{
		
        [Header("Status")]

        /// the current XP of the character
        [MMReadOnly]
		[Tooltip("the current XP of the character")]
		public int CurrentXP ;
		public int MaxXPCap = 35;

		[Header("XP")]

		[MMInformation("Add this component to an object and it'll have XP",MoreMountains.Tools.MMInformationAttribute.InformationType.Info,false)]
		/// the initial amount of XP of the object
		[Tooltip("the initial amount of XP of the object")]
		public int InitialXP = 0;
		/// the maximum amount of XP of the object
		[Tooltip("the maximum amount of XP of the object")]
		public int MaximumXP = 5;
		[Tooltip("the current level given the amount of XP")]
		public int CurrentLevel = 1;
		/// if this is true, XP values will be reset everytime this character is enabled (usually at the start of a scene)
		[Tooltip("if this is true, XP values will be reset everytime this character is enabled (usually at the start of a scene)")]
		public bool ResetXPOnEnable = true;

		[SerializeField] private GUIUpdaterScriptableObject guiUpdater;
        
        /// <summary>
        /// On Start, we initialize our XP
        /// </summary>
        protected void Awake()
	    {
			SetInitialXP();
	    }

        public void SetInitialXP()
        {
		    SetXP(InitialXP);	
        }

		/// <summary>
		/// When the object is enabled (on respawn for example), we restore its initial XP levels
		/// </summary>
	    protected void OnEnable()
		{
			if (ResetXPOnEnable)
			{
				SetInitialXP();
			}
        }

		/// <summary>
		/// Called when the character gets XP from picking up XP Items
		/// </summary>
		/// <param name="XP">The XP the character gets.</param>
		public void GetXP(int XP)
		{
			// Set XP to new XP
			SetXP(Mathf.Min (CurrentXP + XP, MaximumXP));

			// If MaxXP is reached, level up 
			if (CurrentXP == MaximumXP) {
				CurrentLevel++;
				MaximumXP = GetMaximumXP(CurrentLevel);
				CurrentXP = 0;

				// This will update XP bar 0.5s after game resumes
				IEnumerator coroutine = SetXPBarToZero(0.5f);
        		StartCoroutine(coroutine);
			}				
		}

		private IEnumerator SetXPBarToZero(float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			UpdateXPBar();
		}

		/// <summary>
	    /// Change the character's max XP based on current level
	    /// </summary>
	    public int GetMaximumXP(int currentLevel)
	    {
		    return Mathf.Min(MaxXPCap, Mathf.FloorToInt(MaximumXP * 1.2f));
        }

        /// <summary>
        /// Sets the current XP to the specified new value, and updates the XP bar
        /// </summary>
        /// <param name="newValue"></param>
        public void SetXP(int newValue)
        {
            CurrentXP = newValue;
            UpdateXPBar();
        }

	    /// <summary>
	    /// Updates the character's XP bar progress.
	    /// </summary>
		public void UpdateXPBar()
	    {
			guiUpdater.OnXpChange(CurrentXP, MaximumXP);

			if (CurrentXP == MaximumXP)
			{
				guiUpdater.OnLevelUp();
			}
		}
    }
}
