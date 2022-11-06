using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SoundIcon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Image soundSprite;
    [SerializeField] Sprite soundOnIcon;
    [SerializeField] Sprite soundOffIcon;
    [SerializeField] MMSoundManager.MMSoundManagerTracks track;
    private float savedVolume;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Unmute
        if (volumeSlider.value == 0) 
        {
            soundSprite.sprite = soundOnIcon;
            if (savedVolume == 0) 
            {
                savedVolume = 1.0f;
            }
            MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.SetVolumeTrack, track, savedVolume);
            volumeSlider.value = savedVolume;
        } 

        // Mute
        else 
        {
            savedVolume = volumeSlider.value;
            soundSprite.sprite = soundOffIcon;
            MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.SetVolumeTrack, track, 0);
            volumeSlider.value = 0;
        }
    }
}
