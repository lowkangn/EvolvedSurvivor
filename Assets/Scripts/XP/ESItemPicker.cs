using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;

namespace MoreMountains.InventoryEngine
{
    /// <summary>
    /// Add this component to an object so it can be picked and added to an inventory
    /// </summary>
    public class ESItemPicker : ItemPicker
    {
        /// <summary>
        /// Triggered when something collides with the picker
        /// </summary>
        /// <param name="collider">Other.</param>
        public override void OnTriggerEnter2D(Collider2D collider)
        {
            // if what's colliding with the picker ain't a characterBehavior, we do nothing and exit
            if (!collider.CompareTag("ESPickup"))
            {
                return;
            }

            string playerID = "Player1";
            InventoryCharacterIdentifier identifier = collider.gameObject.transform.parent.gameObject.GetComponent<InventoryCharacterIdentifier>();
            if (identifier != null)
            {
                playerID = identifier.PlayerID;
            }

            Pick(Item.TargetInventoryName, playerID);
        }
    }
}