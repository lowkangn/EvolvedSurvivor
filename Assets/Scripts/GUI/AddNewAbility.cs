using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoreMountains.Tools
{
    public class AddNewAbility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject confirmButton;
        public GameObject text;

        public virtual void AddAbility() {
            // Prints the button that was clicked
            // Debug.Log(this.gameObject.transform.parent.parent.name);

            // Set confirm button active
            confirmButton.SetActive(true);
        } 

        //Detect if the Cursor starts to pass over the button
        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            text.GetComponent<Text>().text = "Description";
        }

        //Detect when Cursor leaves the button
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            text.GetComponent<Text>().text = "";
        }
    }
}

