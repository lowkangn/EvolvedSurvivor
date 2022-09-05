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

        /// if this is true, this component will use material property blocks instead of working on an instance of the material.
        [Tooltip("if this is true, this component will use material property blocks instead of working on an instance of the material.")] 
        public bool UseMaterialPropertyBlocks = false;

	    protected MMXPBar _XPBar;
        protected MaterialPropertyBlock _propertyBlock;
        protected bool _hasColorProperty = false;
        
        /// <summary>
        /// On Start, we initialize our XP
        /// </summary>
        protected void Awake()
	    {
			Initialization();
			SetInitialXP();
	    }

        protected void Start()
        {
        }

        public void SetInitialXP()
        {
		    SetXP(InitialXP);	
        }

	    /// <summary>
	    /// Grabs useful components
	    /// </summary>
		public void Initialization()
		{
            _XPBar = this.gameObject.GetComponentInParent<MMXPBar>();
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

			UpdateXPBar(true);

			// If MaxXP is reached, level up 
			if (CurrentXP == MaximumXP) {
				CurrentLevel++;
				MaximumXP = GetMaximumXP(CurrentLevel);
				CurrentXP = 0;
				Debug.Log("CurrentLevel: " + CurrentLevel + ", MaximumXP: " + MaximumXP);

				// This will update XP bar 1s after game resumes
				IEnumerator coroutine = SetXPBarToZero(1.0f);
        		StartCoroutine(coroutine);
			}				
		}

		private IEnumerator SetXPBarToZero(float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			UpdateXPBar(true);
		}

		/// <summary>
	    /// Change the character's max XP based on current level
	    /// </summary>
	    public int GetMaximumXP(int currentLevel)
	    {
		    return MaximumXP + 5; // Change this func after play testing
        }	

	    /// <summary>
	    /// Resets the character's XP to its max value
	    /// </summary>
	    public void ResetXPToMaxXP()
	    {
		    SetXP(MaximumXP);
        }	

        /// <summary>
        /// Sets the current XP to the specified new value, and updates the XP bar
        /// </summary>
        /// <param name="newValue"></param>
        public void SetXP(int newValue)
        {
            CurrentXP = newValue;
            UpdateXPBar(false);
        }

	    /// <summary>
	    /// Updates the character's XP bar progress.
	    /// </summary>
		public void UpdateXPBar(bool show)
	    {
	    	if (_XPBar != null)
	    	{
				_XPBar.UpdateBar(CurrentXP, 0f, MaximumXP, show);
	    	}

			// We update the XP bar
			if (GUIManager.HasInstance)
			{
				GUIManager.Instance.UpdateXPBar(CurrentXP, 0f, MaximumXP);

				if (CurrentXP == MaximumXP) {
					TopDownEngineEvent.Trigger(TopDownEngineEventTypes.LevelUp, null);
				}
			}  
	    }
	}
}