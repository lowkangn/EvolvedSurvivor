using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelpNextButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject[] helpPages;

    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject backButton;

    public void OnPointerClick (PointerEventData eventData)
    {
        for (int i = 0; i < helpPages.Length - 1; i++) {
            if (helpPages[i].activeSelf) { // Go from page i to page i+1
                helpPages[i].SetActive(false);
                helpPages[i + 1].SetActive(true);

                if (i == 0) {
                    backButton.SetActive(true); // Set backButton to true if not on first page
                }
                
                if (i + 1 == helpPages.Length - 1) {
                    nextButton.SetActive(false); // Set nextButton to false if this is the last page
                }
                
                break;
            }
        }
    }
}
