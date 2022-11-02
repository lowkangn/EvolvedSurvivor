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
}
