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
            GameObject colliderObj = collider.gameObject;
            Transform colliderTransform = colliderObj.transform;
            Transform playerTransform = colliderTransform.parent;
            if (playerTransform != null)
            {
                GameObject playerObj = playerTransform.gameObject;
                _collidingObject = playerObj;
                PickItem(playerObj);
            }
        }
    }
}