using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SoundIcon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Image soundSprite;
    [SerializeField] Sprite soundOnIcon;
    [SerializeField] Sprite soundOffIcon;
    private float savedVolume;

    public void OnPointerClick (PointerEventData eventData)
    {
        // Unmute
        if (AudioListener.volume == 0) 
        {
            soundSprite.sprite = soundOnIcon;
            if (savedVolume == 0) 
            {
                savedVolume = 1.0f;
            }
            AudioListener.volume = savedVolume;
        } 

        // Mute
        else 
        {
            savedVolume = AudioListener.volume;
            soundSprite.sprite = soundOffIcon;
            AudioListener.volume = 0;
        }

        volumeSlider.value = AudioListener.volume;
    }
}
