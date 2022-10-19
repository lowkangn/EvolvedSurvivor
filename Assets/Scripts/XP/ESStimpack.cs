using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    public class ESStimpack : Stimpack
    {
        /// <summary>
        /// Triggered when something collides with the coin
        /// </summary>
        /// <param name="collider">Other.</param>
        public override void OnTriggerEnter2D(Collider2D collider)
        {
            GameObject playerObj = collider.gameObject.transform.parent.gameObject;
            _collidingObject = playerObj;
            PickItem(playerObj);
        }
    }
}