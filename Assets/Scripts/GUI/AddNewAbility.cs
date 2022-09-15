using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
    public class AddNewAbility : MonoBehaviour
    {
        public GameObject confirmButton;

        public virtual void AddAbility() {
            // Prints the button that was clicked
            Debug.Log(this.gameObject.transform.parent.parent.name);

            // Set confirm button active
            confirmButton.SetActive(true);
        } 
    }
}

