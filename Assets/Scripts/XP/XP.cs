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

        // [Header("Damage")]

        // [MMInformation("Here you can specify an effect and a sound FX to instantiate when the object gets damaged, and also how long the object should flicker when hit (only works for sprites).", MoreMountains.Tools.MMInformationAttribute.InformationType.Info, false)]
        // /// whether or not this XP object can be damaged 
        // [Tooltip("whether or not this XP object can be damaged")]
        // public bool ImmuneToDamage = false;
        // /// whether or not this object is immune to damage knockback
		// [Tooltip("whether or not this object is immune to damage knockback")]
		// public bool ImmuneToKnockback = false;
		// /// the feedback to play when getting damage
		// [Tooltip("the feedback to play when getting damage")]
		// public MMFeedbacks DamageMMFeedbacks;
		// /// if this is true, the damage value will be passed to the MMFeedbacks as its Intensity parameter, letting you trigger more intense feedbacks as damage increases
		// [Tooltip("if this is true, the damage value will be passed to the MMFeedbacks as its Intensity parameter, letting you trigger more intense feedbacks as damage increases")]
		// public bool FeedbackIsProportionalToDamage = false;
		// /// if you set this to true, other objects damaging this one won't take any self damage
		// [Tooltip("if you set this to true, other objects damaging this one won't take any self damage")]
		// public bool PreventTakeSelfDamage = false;


        // [Header("Death")]

        // [MMInformation("Here you can set an effect to instantiate when the object dies, a force to apply to it (topdown controller required), how many points to add to the game score, and where the character should respawn (for non-player characters only).", MoreMountains.Tools.MMInformationAttribute.InformationType.Info, false)]
        // /// whether or not this object should get destroyed on death
        // [Tooltip("whether or not this object should get destroyed on death")]
		// public bool DestroyOnDeath = true;
        // /// the time (in seconds) before the character is destroyed or disabled
        // [Tooltip("the time (in seconds) before the character is destroyed or disabled")]
		// public float DelayBeforeDestruction = 0f;
		// /// the points the player gets when the object's XP reaches zero
		// [Tooltip("the points the player gets when the object's XP reaches zero")]
		// public int PointsWhenDestroyed;
		// /// if this is set to false, the character will respawn at the location of its death, otherwise it'll be moved to its initial position (when the scene started)
		// [Tooltip("if this is set to false, the character will respawn at the location of its death, otherwise it'll be moved to its initial position (when the scene started)")]
		// public bool RespawnAtInitialLocation = false;
		// /// if this is true, the controller will be disabled on death
		// [Tooltip("if this is true, the controller will be disabled on death")]
		// public bool DisableControllerOnDeath = true;
		// /// if this is true, the model will be disabled instantly on death (if a model has been set)
		// [Tooltip("if this is true, the model will be disabled instantly on death (if a model has been set)")]
		// public bool DisableModelOnDeath = true;
		// /// if this is true, collisions will be turned off when the character dies
		// [Tooltip("if this is true, collisions will be turned off when the character dies")]
		// public bool DisableCollisionsOnDeath = true;
		// /// if this is true, collisions will also be turned off on child colliders when the character dies
		// [Tooltip("if this is true, collisions will also be turned off on child colliders when the character dies")]
		// public bool DisableChildCollisionsOnDeath = false;
        // /// whether or not this object should change layer on death
        // [Tooltip("whether or not this object should change layer on death")]
        // public bool ChangeLayerOnDeath = false;
        // /// whether or not this object should change layer on death
        // [Tooltip("whether or not this object should change layer on death")]
        // public bool ChangeLayersRecursivelyOnDeath = false;
        // /// the layer we should move this character to on death
        // [Tooltip("the layer we should move this character to on death")]
        // public MMLayer LayerOnDeath;
        // /// the feedback to play when dying
        // [Tooltip("the feedback to play when dying")]
		// public MMFeedbacks DeathMMFeedbacks;

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
        
        // [Header("Shared XP")]
        // /// another XP component (usually on another character) towards which all XP will be redirected
        // [Tooltip("another XP component (usually on another character) towards which all XP will be redirected")]
        // public XP MasterXP;

        // [Header("Animator")] 
        // /// the target animator to pass a Death animation parameter to. The XP component will try to auto bind this if left empty
        // [Tooltip("the target animator to pass a Death animation parameter to. The XP component will try to auto bind this if left empty")]
        // public Animator TargetAnimator;
        // /// if this is true, animator logs for the associated animator will be turned off to avoid potential spam
        // [Tooltip("if this is true, animator logs for the associated animator will be turned off to avoid potential spam")]
        // public bool DisableAnimatorLogs = true;
        
        // public int LastDamage { get; set; }
        // public Vector3 LastDamageDirection { get; set; }

        // // hit delegate
        // public delegate void OnHitDelegate();
        // public OnHitDelegate OnHit;

        // // respawn delegate
        // public delegate void OnReviveDelegate();
		// public OnReviveDelegate OnRevive;

        // // death delegate
		// public delegate void OnDeathDelegate();
		// public OnDeathDelegate OnDeath;

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
	        // GrabAnimator();
        }

        public virtual void SetInitialXP()
        {
	        // if (MasterXP == null)
	        // {
		        SetXP(InitialXP);	
	        // }
	        // else
	        // {
		    //     CurrentXP = MasterXP.CurrentXP;
	        // }
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
	            
	            // if (ResetColorOnRevive)
	            // {
		        //     if (UseMaterialPropertyBlocks)
		        //     {
			    //         if (_renderer.sharedMaterial.HasProperty(ColorMaterialPropertyName))
			    //         {
				//             _hasColorProperty = true; 
				//             _initialColor = _renderer.sharedMaterial.GetColor(ColorMaterialPropertyName);
			    //         }
		        //     }
		        //     else
		        //     {
			    //         if (_renderer.material.HasProperty(ColorMaterialPropertyName))
			    //         {
				//             _hasColorProperty = true;
				//             _initialColor = _renderer.material.GetColor(ColorMaterialPropertyName);
			    //         } 
		        //     }
	            // }
            }

            _initialLayer = gameObject.layer;

            _autoRespawn = this.gameObject.GetComponentInParent<AutoRespawn>();
            _XPBar = this.gameObject.GetComponentInParent<MMXPBar>();
            _controller = this.gameObject.GetComponentInParent<TopDownController>();
            _characterController = this.gameObject.GetComponentInParent<CharacterController>();
            _collider2D = this.gameObject.GetComponentInParent<Collider2D>();
            _collider3D = this.gameObject.GetComponentInParent<Collider>();

            // DamageMMFeedbacks?.Initialization(this.gameObject);
            // DeathMMFeedbacks?.Initialization(this.gameObject);

            StoreInitialPosition();
			_initialized = true;
			
			// DamageEnabled();
		}
	    
	    // protected virtual void GrabAnimator()
	    // {
		//     if (TargetAnimator == null)
		//     {
		// 	    BindAnimator();
		//     }

		//     if ((TargetAnimator != null) && DisableAnimatorLogs)
		//     {
		// 	    TargetAnimator.logWarnings = false;
		//     }
	    // }

	    // protected virtual void BindAnimator()
	    // {
		//     if (_character != null)
		//     {
		// 	    if (_character.CharacterAnimator != null)
		// 	    {
		// 		    TargetAnimator = _character.CharacterAnimator;
		// 	    }
		// 	    else
		// 	    {
		// 		    TargetAnimator = GetComponent<Animator>();
		// 	    }
		//     }
		//     else
		//     {
		// 	    TargetAnimator = GetComponent<Animator>();
		//     }    
	    // }

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
			// DamageEnabled();
	    }

		// /// <summary>
		// /// Called when the object takes damage
		// /// </summary>
		// /// <param name="damage">The amount of XP points that will get lost.</param>
		// /// <param name="instigator">The object that caused the damage.</param>
		// /// <param name="flickerDuration">The time (in seconds) the object should flicker after taking the damage.</param>
		// /// <param name="invincibilityDuration">The duration of the short invincibility following the hit.</param>
		// public virtual void Damage(int damage, GameObject instigator, float flickerDuration, float invincibilityDuration, Vector3 damageDirection)
		// {
		// 	// if the object is invulnerable, we do nothing and exit
		// 	if (Invulnerable || ImmuneToDamage)
		// 	{
		// 		return;
		// 	}

		// 	if (!this.enabled)
		// 	{
		// 		return;
		// 	}
			
		// 	// if we're already below zero, we do nothing and exit
		// 	if ((CurrentXP <= 0) && (InitialXP != 0))
		// 	{
		// 		return;
		// 	}
			
		// 	// we decrease the character's XP by the damage
		// 	float previousXP = CurrentXP;
		// 	if (MasterXP != null)
		// 	{
		// 		previousXP = MasterXP.CurrentXP;
		// 		MasterXP.SetXP(MasterXP.CurrentXP - damage);
		// 	}
		// 	else
		// 	{
		// 		SetXP(CurrentXP - damage);	
		// 	}

		// 	LastDamage = damage;
        //     LastDamageDirection = damageDirection;
        //     if (OnHit != null)
        //     {
        //         OnHit();
        //     }

        //     // we prevent the character from colliding with Projectiles, Player and Enemies
        //     if (invincibilityDuration > 0)
		// 	{
		// 		DamageDisabled();
		// 		StartCoroutine(DamageEnabled(invincibilityDuration));	
		// 	}
            
		// 	// we trigger a damage taken event
		// 	MMDamageTakenEvent.Trigger(_character, instigator, CurrentXP, damage, previousXP);

        //     if (TargetAnimator != null)
        //     {
        //         TargetAnimator.SetTrigger("Damage");
        //     }

        //     if (FeedbackIsProportionalToDamage)
        //     {
	    //         DamageMMFeedbacks?.PlayFeedbacks(this.transform.position, damage);    
        //     }
        //     else
        //     {
	    //         DamageMMFeedbacks?.PlayFeedbacks(this.transform.position);
        //     }
            
		// 	// we update the XP bar
		// 	UpdateXPBar(true);

		// 	// if XP has reached zero we set its XP to zero (useful for the XPbar)
		// 	if (MasterXP != null)
		// 	{
		// 		if (MasterXP.CurrentXP <= 0)
		// 		{
		// 			MasterXP.CurrentXP = 0;
		// 			MasterXP.Kill();
		// 		}
		// 	}
		// 	else
		// 	{
		// 		if (CurrentXP <= 0)
		// 		{
		// 			CurrentXP = 0;
		// 			Kill();
		// 		}
					
		// 	}
		// }

		// /// <summary>
		// /// Kills the character, instantiates death effects, handles points, etc
		// /// </summary>
		// public virtual void Kill()
        // {
	    //     if (ImmuneToDamage)
	    //     {
		//         return;
	    //     }
	        
        //     if (_character != null)
        //     {
        //         // we set its dead state to true
        //         _character.ConditionState.ChangeState(CharacterStates.CharacterConditions.Dead);
        //         _character.Reset();

        //         if (_character.CharacterType == Character.CharacterTypes.Player)
        //         {
        //             TopDownEngineEvent.Trigger(TopDownEngineEventTypes.PlayerDeath, _character);
        //         }
        //     }
        //     SetXP(0);

        //     // we prevent further damage
        //     DamageDisabled();

        //     DeathMMFeedbacks?.PlayFeedbacks(this.transform.position);
            
		// 	// Adds points if needed.
		// 	if(PointsWhenDestroyed != 0)
		// 	{
		// 		// we send a new points event for the GameManager to catch (and other classes that may listen to it too)
		// 		TopDownEnginePointEvent.Trigger(PointsMethods.Add, PointsWhenDestroyed);
		// 	}

        //     if (TargetAnimator != null)
        //     {
        //         TargetAnimator.SetTrigger("Death");
        //     }
        //     // we make it ignore the collisions from now on
        //     if (DisableCollisionsOnDeath)
        //     {
        //         if (_collider2D != null)
        //         {
        //             _collider2D.enabled = false;
        //         }
        //         if (_collider3D != null)
        //         {
        //             _collider3D.enabled = false;
        //         }

        //         // if we have a controller, removes collisions, restores parameters for a potential respawn, and applies a death force
        //         if (_controller != null)
		// 	    {				
		// 			_controller.CollisionsOff();						
        //         }

        //         if (DisableChildCollisionsOnDeath)
        //         {
        //             foreach (Collider2D collider in this.gameObject.GetComponentsInChildren<Collider2D>())
        //             {
        //                 collider.enabled = false;
        //             }
        //             foreach (Collider collider in this.gameObject.GetComponentsInChildren<Collider>())
        //             {
        //                 collider.enabled = false;
        //             }
        //         }
        //     }

        //     if (ChangeLayerOnDeath)
        //     {
        //         gameObject.layer = LayerOnDeath.LayerIndex;
        //         if (ChangeLayersRecursivelyOnDeath)
        //         {
        //             this.transform.ChangeLayersRecursively(LayerOnDeath.LayerIndex);
        //         }
        //     }
            
        //     OnDeath?.Invoke();
        //     MMLifeCycleEvent.Trigger(this, MMLifeCycleEventTypes.Death);

        //     if (DisableControllerOnDeath && (_controller != null))
        //     {
        //         _controller.enabled = false;
        //     }

        //     if (DisableControllerOnDeath && (_characterController != null))
        //     {
        //         _characterController.enabled = false;
        //     }

        //     if (DisableModelOnDeath && (Model != null))
        //     {
        //         Model.SetActive(false);
        //     }

		// 	if (DelayBeforeDestruction > 0f)
		// 	{
		// 		Invoke ("DestroyObject", DelayBeforeDestruction);
		// 	}
		// 	else
		// 	{
		// 		// finally we destroy the object
		// 		DestroyObject();	
		// 	}
		// }

		// /// <summary>
		// /// Revive this object.
		// /// </summary>
		// public virtual void Revive()
		// {
		// 	if (!_initialized)
		// 	{
		// 		return;
		// 	}

        //     if (_collider2D != null)
        //     {
        //         _collider2D.enabled = true;
        //     }
        //     if (_collider3D != null)
        //     {
        //         _collider3D.enabled = true;
        //     }
        //     if (DisableChildCollisionsOnDeath)
        //     {
        //         foreach (Collider2D collider in this.gameObject.GetComponentsInChildren<Collider2D>())
        //         {
        //             collider.enabled = true;
        //         }
        //         foreach (Collider collider in this.gameObject.GetComponentsInChildren<Collider>())
        //         {
        //             collider.enabled = true;
        //         }
        //     }
        //     if (ChangeLayerOnDeath)
        //     {
        //         gameObject.layer = _initialLayer;
        //         if (ChangeLayersRecursivelyOnDeath)
        //         {
        //             this.transform.ChangeLayersRecursively(_initialLayer);
        //         }
        //     }
        //     if (_characterController != null)
        //     {
        //         _characterController.enabled = true;
        //     }
        //     if (_controller != null)
		// 	{
        //         _controller.enabled = true;
		// 		_controller.CollisionsOn();
		// 		_controller.Reset();
		// 	}
		// 	if (_character != null)
		// 	{
		// 		_character.ConditionState.ChangeState(CharacterStates.CharacterConditions.Normal);
		// 	}
        //     if (ResetColorOnRevive && (_renderer != null))
        //     {
	    //         if (UseMaterialPropertyBlocks)
	    //         {
		//             _renderer.GetPropertyBlock(_propertyBlock);
		//             _propertyBlock.SetColor(ColorMaterialPropertyName, _initialColor);
		//             _renderer.SetPropertyBlock(_propertyBlock);    
	    //         }
	    //         else
	    //         {
		//             _renderer.material.SetColor(ColorMaterialPropertyName, _initialColor);
	    //         }
        //     }            

        //     if (RespawnAtInitialLocation)
		// 	{
		// 		transform.position = _initialPosition;
		// 	}
        //     if (_XPBar != null)
        //     {
        //         _XPBar.Initialization();
        //     }

        //     Initialization();
        //     SetInitialXP();
        //     OnRevive?.Invoke();
        //     MMLifeCycleEvent.Trigger(this, MMLifeCycleEventTypes.Revive);
        // }

	    // /// <summary>
	    // /// Destroys the object, or tries to, depending on the character's settings
	    // /// </summary>
	    // protected virtual void DestroyObject()
        // {
        //     if (_autoRespawn == null)
        //     {
        //         if (DestroyOnDeath)
        //         {
        //             gameObject.SetActive(false);
        //         }                
        //     }
        //     else
        //     {
        //         _autoRespawn.Kill();
        //     }
        // }

		/// <summary>
		/// Called when the character gets XP from picking up XP Items
		/// </summary>
		/// <param name="XP">The XP the character gets.</param>
		// / <param name="instigator">The thing that gives the character XP.</param>
		public virtual void GetXP(int XP)
		{
			// this function adds XP to the character's XP and prevents it to go above MaxXP.
			// if (MasterXP != null)
			// {
			// 	MasterXP.SetXP(Mathf.Min (CurrentXP + XP, MaximumXP));	
			// }
			// else
			// {
				// If MaxXP is reached, level up 
				if (CurrentXP == MaximumXP) {
					CurrentLevel++;
					MaximumXP = GetMaximumXP(CurrentLevel);
					CurrentXP = 0;
					Debug.Log("CurrentLevel: " + CurrentLevel + ", MaximumXP: " + MaximumXP);
				}	

				// Set XP to new XP
				SetXP(Mathf.Min (CurrentXP + XP, MaximumXP));
			// }
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

	        // if (MasterXP == null)
	        // {
		        if (_character != null)
		        {
			        if (_character.CharacterType == Character.CharacterTypes.Player)
			        {
				        // We update the XP bar
				        if (GUIManager.HasInstance)
				        {
					        GUIManager.Instance.UpdateXPBar(CurrentXP, 0f, MaximumXP, _character.PlayerID);
				        }
			        }
		        }    
	        // }
	    }

	    // /// <summary>
	    // /// Prevents the character from taking any damage
	    // /// </summary>
	    // public virtual void DamageDisabled()
	    // {
		// 	Invulnerable = true;
	    // }

	    // /// <summary>
	    // /// Allows the character to take damage
	    // /// </summary>
	    // public virtual void DamageEnabled()
	    // {
	    // 	Invulnerable = false;
	    // }

		// /// <summary>
	    // /// makes the character able to take damage again after the specified delay
	    // /// </summary>
	    // /// <returns>The layer collision.</returns>
	    // public virtual IEnumerator DamageEnabled(float delay)
		// {
		// 	yield return new WaitForSeconds (delay);
		// 	Invulnerable = false;
		// }

        // /// <summary>
        // /// On Disable, we prevent any delayed destruction from running
        // /// </summary>
        // protected virtual void OnDisable()
        // {
        //     CancelInvoke();
        // }
	}
}