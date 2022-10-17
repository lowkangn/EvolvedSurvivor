using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ESSoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Image soundSprite;
    [SerializeField] Sprite soundOnIcon;
    [SerializeField] Sprite soundOffIcon;

    void Start()
    {
        volumeSlider.value = AudioListener.volume;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        if (AudioListener.volume == 0) 
        {
            soundSprite.sprite = soundOffIcon;
        }
        if (AudioListener.volume > 0) 
        {
            soundSprite.sprite = soundOnIcon;
        }
    }
}
