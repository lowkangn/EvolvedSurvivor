using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathSplashText : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private KillCounter killCounter;

    [SerializeField] private Text timeSurvivedText;
    [SerializeField] private Text enemiesKilledText;

    void Awake() {
        this.timeSurvivedText.text += timer.GetFinalTime();
        this.enemiesKilledText.text += killCounter.GetEnemiesKilled();
    }

}
