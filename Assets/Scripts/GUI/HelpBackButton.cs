using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelpBackButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject helpPage1;
    [SerializeField] private GameObject helpPage2;
    [SerializeField] private GameObject helpPage3;

    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject backButton;

    public void OnPointerClick (PointerEventData eventData)
    {
        if (helpPage2.activeSelf) { // Go from page 2 to 1
            helpPage2.SetActive(false);
            helpPage1.SetActive(true);
            backButton.SetActive(false); // Set self to false if going to the first page
        } else if (helpPage3.activeSelf) {
            helpPage3.SetActive(false);
            helpPage2.SetActive(true);
            nextButton.SetActive(true); // Set nextButton to true if leaving the last page 
        }
    }
}
