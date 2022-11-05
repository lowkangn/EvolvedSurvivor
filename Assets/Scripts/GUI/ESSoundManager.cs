using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

public class ESSoundManager : MonoBehaviour
{
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Image masterSoundSprite;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Image musicSoundSprite;
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] Image sfxSoundSprite;
    [SerializeField] Slider uiVolumeSlider;
    [SerializeField] Image uiSoundSprite;
    [SerializeField] Sprite soundOnIcon;
    [SerializeField] Sprite soundOffIcon;

    void Awake()
    {
        MMSoundManager soundManager = GameObject.Find("SoundManager").GetComponent<MMSoundManager>();
        SetMasterVolume(soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Master, false));
        SetMusicVolume(soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, false));
        SetSfxVolume(soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Sfx, false));
        SetUiVolume(soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.UI, false));
    }

    private void ChangeVolume(MMSoundManager.MMSoundManagerTracks track, float volume, Image spriteImage)
    {
        MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.SetVolumeTrack, track, volume);

        if (volume == 0)
        {
            spriteImage.sprite = soundOffIcon;
        }
        if (volume > 0)
        {
            spriteImage.sprite = soundOnIcon;
        }
    }

    public void ChangeMasterVolume()
    {
        ChangeVolume(MMSoundManager.MMSoundManagerTracks.Master, masterVolumeSlider.value, masterSoundSprite);
    }

    public void ChangeMusicVolume()
    {
        ChangeVolume(MMSoundManager.MMSoundManagerTracks.Music, musicVolumeSlider.value, musicSoundSprite);
    }

    public void ChangeSfxVolume()
    {
        ChangeVolume(MMSoundManager.MMSoundManagerTracks.Sfx, sfxVolumeSlider.value, sfxSoundSprite);
    }

    public void ChangeUiVolume()
    {
        ChangeVolume(MMSoundManager.MMSoundManagerTracks.UI, uiVolumeSlider.value, uiSoundSprite);
    }

    private void SetMasterVolume(float newVolume)
    {
        masterVolumeSlider.value = newVolume;

        if (newVolume == 0)
        {
            masterSoundSprite.sprite = soundOffIcon;
        }
        if (newVolume > 0)
        {
            masterSoundSprite.sprite = soundOnIcon;
        }
    }

    private void SetMusicVolume(float newVolume)
    {
        musicVolumeSlider.value = newVolume;

        if (newVolume == 0)
        {
            musicSoundSprite.sprite = soundOffIcon;
        }
        if (newVolume > 0)
        {
            musicSoundSprite.sprite = soundOnIcon;
        }
    }

    private void SetSfxVolume(float newVolume)
    {
        sfxVolumeSlider.value = newVolume;

        if (newVolume == 0)
        {
            sfxSoundSprite.sprite = soundOffIcon;
        }
        if (newVolume > 0)
        {
            sfxSoundSprite.sprite = soundOnIcon;
        }
    }

    private void SetUiVolume(float newVolume)
    {
        uiVolumeSlider.value = newVolume;

        if (newVolume == 0)
        {
            uiSoundSprite.sprite = soundOffIcon;
        }
        if (newVolume > 0)
        {
            uiSoundSprite.sprite = soundOnIcon;
        }
    }
}
