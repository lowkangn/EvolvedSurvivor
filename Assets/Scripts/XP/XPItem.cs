using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
// using System;

namespace MoreMountains.TopDownEngine
{	
	// [CreateAssetMenu(fileName = "XPItem", menuName = "MoreMountains/InventoryEngine/XPItem", order = 1)]
	// [Serializable]
	/// <summary>
	/// Demo class for a XP item
	/// </summary>
    [AddComponentMenu("TopDown Engine/Items/XP")]
	public class XPItem : PickableItem 
	{
		[Header("XP Bonus")]
		/// the amount of XP to add to the player when the item is used
		public int XPBonus;
        private GameObject player;

        void Awake() {
            player = GameObject.FindGameObjectWithTag("Player");
        }

		/// <summary>
		/// Triggered when something collides with the XP item
		/// </summary>
		/// <param name="collider">Other.</param>
		protected override void Pick(GameObject picker) 
		{
            TopDownEngine.XP playerXP = player.GetComponent<TopDownEngine.XP>();
			playerXP.GetXP(XPBonus);
			// Destroy(gameObject);
            // Debug.Log("Total XP is " + playerXP.CurrentXP);
		}		
	}
}