using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

namespace MoreMountains.TopDownEngine
{
	public struct XPChangeEvent
	{
		public XP AffectedXP;
		public int NewXP;
		
		public XPChangeEvent(XP affectedXP, int newXP)
		{
			AffectedXP = affectedXP;
			NewXP = newXP;
		}

		static XPChangeEvent e;
		public static void Trigger(XP affectedXP, int newXP)
		{
			e.AffectedXP = affectedXP;
			e.NewXP = newXP;
			MMEventManager.TriggerEvent(e);
		}
	}
	
	/// <summary>
	/// This class manages the XP of an object, pilots its potential XP bar, handles what happens when it takes damage,
	/// and what happens when it dies.
	/// </summary>
	[AddComponentMenu("TopDown Engine/Character/Core/XP")] 
	public class XP : MonoBehaviour
	{
        [Header("Bindings")]

		/// the model to disable (if set so)
		[Tooltip("the model to disable (if set so)")]
		public GameObject Model;
		
        [Header("Status")]

        /// the current XP of the character
        [MMReadOnly]
		[Tooltip("the current XP of the character")]
		public int CurrentXP ;

		[Header("XP")]

		[MMInformation("Add this component to an object and it'll have XP, will be able to get damaged and potentially die.",MoreMountains.Tools.MMInformationAttribute.InformationType.Info,false)]
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

        // /// if this is true, color will be reset on revive
        // [Tooltip("if this is true, color will be reset on revive")]
        // public bool ResetColorOnRevive = true;
        // /// the name of the property on your renderer's shader that defines its color 
        // [Tooltip("the name of the property on your renderer's shader that defines its color")]
        // [MMCondition("ResetColorOnRevive", true)]
        // public string ColorMaterialPropertyName = "_Color";
        /// if this is true, this component will use material property blocks instead of working on an instance of the material.
        [Tooltip("if this is true, this component will use material property blocks instead of working on an instance of the material.")] 
        public bool UseMaterialPropertyBlocks = false;

		protected Vector3 _initialPosition;
		protected Renderer _renderer;
		protected Character _character;
		protected TopDownController _controller;
	    protected MMXPBar _XPBar;
	    protected Collider2D _collider2D;
        protected Collider _collider3D;
        protected CharacterController _characterController;
        protected bool _initialized = false;
		protected Color _initialColor;
        protected AutoRespawn _autoRespawn;
        protected int _initialLayer;
        protected MaterialPropertyBlock _propertyBlock;
        protected bool _hasColorProperty = false;
        
        /// <summary>
        /// On Start, we initialize our XP
        /// </summary>
        protected virtual void Awake()
	    {
			Initialization();
			SetInitialXP();
	    }

        protected virtual void Start()
        {
        }

        public virtual void SetInitialXP()
        {
		    SetXP(InitialXP);	
        }

	    /// <summary>
	    /// Grabs useful components, enables damage and gets the inital color
	    /// </summary>
		public virtual void Initialization()
		{
			_character = this.gameObject.GetComponentInParent<Character>(); 

            if (Model != null)
            {
                Model.SetActive(true);
            }        
            
            if (gameObject.GetComponentInParent<Renderer>() != null)
			{
				_renderer = GetComponentInParent<Renderer>();				
			}
			if (_character != null)
			{
				if (_character.CharacterModel != null)
				{
					if (_character.CharacterModel.GetComponentInChildren<Renderer> ()!= null)
					{
						_renderer = _character.CharacterModel.GetComponentInChildren<Renderer> ();	
					}
				}	
			}
            if (_renderer != null)
            {
	            if (UseMaterialPropertyBlocks && (_propertyBlock == null))
	            {
		            _propertyBlock = new MaterialPropertyBlock();
	            }
            }

            _initialLayer = gameObject.layer;

            _autoRespawn = this.gameObject.GetComponentInParent<AutoRespawn>();
            _XPBar = this.gameObject.GetComponentInParent<MMXPBar>();
            _controller = this.gameObject.GetComponentInParent<TopDownController>();
            _characterController = this.gameObject.GetComponentInParent<CharacterController>();
            _collider2D = this.gameObject.GetComponentInParent<Collider2D>();
            _collider3D = this.gameObject.GetComponentInParent<Collider>();

            StoreInitialPosition();
			_initialized = true;
		}

	    /// <summary>
	    /// Stores the initial position for further use
	    /// </summary>
	    public virtual void StoreInitialPosition()
	    {
		    _initialPosition = this.transform.position;
	    }

		/// <summary>
		/// When the object is enabled (on respawn for example), we restore its initial XP levels
		/// </summary>
	    protected virtual void OnEnable()
		{
			if (ResetXPOnEnable)
			{
				SetInitialXP();
			}
			if (Model != null)
            {
                Model.SetActive(true);
            }            
	    }    

		/// <summary>
		/// Called when the character gets XP from picking up XP Items
		/// </summary>
		/// <param name="XP">The XP the character gets.</param>
		public virtual void GetXP(int XP)
		{
			// Set XP to new XP
			SetXP(Mathf.Min (CurrentXP + XP, MaximumXP));

			// If MaxXP is reached, level up 
			if (CurrentXP == MaximumXP) {
				CurrentLevel++;
				MaximumXP = GetMaximumXP(CurrentLevel);
				CurrentXP = 0;
				Debug.Log("CurrentLevel: " + CurrentLevel + ", MaximumXP: " + MaximumXP);
			}	

			UpdateXPBar(true);
		}

		/// <summary>
	    /// Change the character's max XP based on current level
	    /// </summary>
	    public virtual int GetMaximumXP(int currentLevel)
	    {
		    return MaximumXP + 5; // Change this func after play testing
        }	

	    /// <summary>
	    /// Resets the character's XP to its max value
	    /// </summary>
	    public virtual void ResetXPToMaxXP()
	    {
		    SetXP(MaximumXP);
        }	

        /// <summary>
        /// Sets the current XP to the specified new value, and updates the XP bar
        /// </summary>
        /// <param name="newValue"></param>
        public virtual void SetXP(int newValue)
        {
            CurrentXP = newValue;
            UpdateXPBar(false);
            XPChangeEvent.Trigger(this, newValue);
        }

	    /// <summary>
	    /// Updates the character's XP bar progress.
	    /// </summary>
		public virtual void UpdateXPBar(bool show)
	    {
	    	if (_XPBar != null)
	    	{
				_XPBar.UpdateBar(CurrentXP, 0f, MaximumXP, show);
	    	}

			if (_character != null)
			{
				if (_character.CharacterType == Character.CharacterTypes.Player)
				{
					// We update the XP bar
					if (GUIManager.HasInstance)
					{
						GUIManager.Instance.UpdateXPBar(CurrentXP, 0f, MaximumXP, _character.PlayerID);

						if (CurrentXP == MaximumXP) {
							TopDownEngineEvent.Trigger(TopDownEngineEventTypes.LevelUp, null);
						}
					}
				}
			}    
	    }
	}
}