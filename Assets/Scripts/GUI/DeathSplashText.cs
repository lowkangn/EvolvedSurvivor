using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathSplashText : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private Text timeSurvivedText;

    void Awake() {
        this.timeSurvivedText.text += timer.GetFinalTime();
    }

}
