using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelpBackButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject[] helpPages;

    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject backButton;

    public void OnPointerClick (PointerEventData eventData)
    {
        for (int i = 1; i < helpPages.Length; i++) {
            if (helpPages[i].activeSelf) { // Go from page i to page i-1
                helpPages[i].SetActive(false);
                helpPages[i - 1].SetActive(true);

                if (i - 1 == 0) {
                    backButton.SetActive(false); // Set backButton to false if going to the first page
                }

                if (i == helpPages.Length - 1) {
                    nextButton.SetActive(true); // Set nextButton to true if leaving the last page 
                }

                break;
            }
        }
    }
}
