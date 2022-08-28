using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.InventoryEngine;

namespace MoreMountains.TopDownEngine
{	
	[CreateAssetMenu(fileName = "InventoryEngineXP", menuName = "MoreMountains/TopDownEngine/InventoryEngineXP", order = 1)]
	[Serializable]
	/// <summary>
	/// Pickable XP item
	/// </summary>
	public class InventoryEngineXP : InventoryItem 
	{
		// [Header("XP")]
		// [MMInformation("Here you can specify the amount of XP gained after picking up this item.",MMInformationAttribute.InformationType.Info,false)]
		/// the amount of XP to add to the player when the item is picked up
		// [Tooltip("the amount of XP to add to the player when the item is picked up")]
		// public int XPBonus;

		/// <summary>
		/// When the item is used, we try to grab our character's XP component, and if it exists, we add our XP bonus amount
		/// </summary>
		public override bool Use(string playerID)
		{
			base.Use(playerID);

			if (TargetInventory(playerID).Owner == null)
			{
				return false;
			}

			XP characterXP = TargetInventory(playerID).Owner.GetComponent<XP>();

			if (characterXP != null)
			{
				// characterXP.GetXP(XPBonus); //,TargetInventory(playerID).gameObject);
                return true;
			}
            else
            {
                return false;
            }
		}

	}
}