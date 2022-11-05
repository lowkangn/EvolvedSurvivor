using MoreMountains.Tools;
using System.Collections;
using UnityEngine;

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
		public int CurrentXP;
        public int MaxXPCap = 80;
		public int MaxLevel = 80;

        [Header("XP")]

		[MMInformation("Add this component to an object and it'll have XP",MoreMountains.Tools.MMInformationAttribute.InformationType.Info,false)]
		/// the initial amount of XP of the object
		[Tooltip("the initial amount of XP of the object")]
		public int InitialXP = 0;
		/// the maximum amount of XP of the object
		[Tooltip("the maximum amount of XP of the object")]
		public int MaximumXP = 5;
		/// if this is true, XP values will be reset everytime this character is enabled (usually at the start of a scene)
		[Tooltip("if this is true, XP values will be reset everytime this character is enabled (usually at the start of a scene)")]
		public bool ResetXPOnEnable = true;

		[SerializeField] private GUIUpdaterScriptableObject guiUpdater;
		[SerializeField] private ParticleSystem levelUpEffect;

		private int currentLevel = 0;
        
        /// <summary>
        /// On Start, we initialize our XP
        /// </summary>
        private void Awake()
	    {
			SetInitialXP();
	    }

		private void OnDisable()
		{
			StopAllCoroutines();
		}

		public void SetInitialXP()
        {
			UpdateXP(InitialXP);
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
		public void UpdateXP(int XP)
		{
			if (currentLevel >= MaxLevel)
			{
				return;
			}
            // Set XP to new XP
            CurrentXP = Mathf.Min (CurrentXP + XP, MaximumXP);
            guiUpdater.OnXpChange(CurrentXP, MaximumXP);

            // If MaxXP is reached, level up 
            if (CurrentXP == MaximumXP) {
				currentLevel++;
				MaximumXP = GetNextLevelXP();
				CurrentXP = 0;

				IEnumerator coroutine = LevelUp(1f);
        		StartCoroutine(coroutine);
			}				
		}

		private IEnumerator LevelUp(float waitTime)
		{
			levelUpEffect.Play();

            yield return new WaitForSeconds(waitTime);
            guiUpdater.OnLevelUp();
            guiUpdater.OnXpChange(CurrentXP, MaximumXP);
        }

		/// <summary>
	    /// Change the character's max XP based on current level
	    /// </summary>
	    public int GetNextLevelXP()
	    {
		    return Mathf.Min(MaxXPCap, Mathf.FloorToInt(MaximumXP * 1.2f));
        }
    }
}
