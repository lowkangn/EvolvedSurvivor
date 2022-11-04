using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenuEnabled : MonoBehaviour
{
    [SerializeField] GameObject[] helpPages;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject backButton;

    void OnEnable() {
        helpPages[0].SetActive(true);
        for (int i = 1; i < helpPages.Length; i++) {
            helpPages[i].SetActive(false);
        }
        nextButton.SetActive(true);
        backButton.SetActive(false);
    }

    public void goToNextPage() {
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

    public void goToPrevPage() {
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
