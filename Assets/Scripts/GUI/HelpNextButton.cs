using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelpNextButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject helpPage1;
    [SerializeField] private GameObject helpPage2;
    [SerializeField] private GameObject helpPage3;

    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject backButton;

    public void OnPointerClick (PointerEventData eventData)
    {
        if (helpPage1.activeSelf) { // Go from Page 1 to 2
            helpPage1.SetActive(false);
            helpPage2.SetActive(true);
            backButton.SetActive(true); // Set back to true if not on first page
        } else if (helpPage2.activeSelf) {
            helpPage2.SetActive(false);
            helpPage3.SetActive(true);
            nextButton.SetActive(false); // Set self to false if this is the last page
        }
    }
}
