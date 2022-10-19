using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsSaveButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject optionsPanel;

    public void OnPointerClick (PointerEventData eventData)
    {
        optionsPanel.SetActive(false);
    }
}
