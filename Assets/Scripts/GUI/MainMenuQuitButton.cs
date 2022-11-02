using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuQuitButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick (PointerEventData eventData)
    {
        Application.Quit();
    }
}
