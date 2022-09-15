using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverText : MonoBehaviour
{
    public GameObject text;

    void OnMouseOver()
    {
        text.GetComponent<Text>().text = "Description";
    }

    void OnMouseExit()
    {
        text.GetComponent<Text>().text = "";
    }
}
