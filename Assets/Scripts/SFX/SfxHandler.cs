using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class SfxHandler : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField]
    protected AudioClip sfx;
    [SerializeField]
    protected float volume = 1.0f;
    [SerializeField]
    protected bool loop;
    [SerializeField]
    protected int id;

    private MMSoundManagerPlayOptions options = MMSoundManagerPlayOptions.Default;
    private bool isPlaying;

    void Awake()
    {
        InitialiseSfx();
    }

    private void InitialiseSfx()
    {
        options.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Sfx;
        options.Location = this.transform.position;
        options.Volume = volume;
        options.Loop = loop;
        options.ID = id;
    }

    public void PlaySfx()
    {
        if (id != 0)
        {
            MMSoundManagerSoundControlEvent.Trigger(MMSoundManagerSoundControlEventTypes.Stop, id);
        }
        else {
            isPlaying = false;
        }

        if (!isPlaying)
        {
            isPlaying = true;
            MMSoundManagerSoundPlayEvent.Trigger(sfx, options);
        }
    }

    public void StopSfx()
    {
        if (id != 0) // id == 0 means ID not assigned, so should not be calling this method (this method is only used for sfx that need to be stopped prematurely and ID must be assigned)
        {
            MMSoundManagerSoundControlEvent.Trigger(MMSoundManagerSoundControlEventTypes.Stop, id);
            isPlaying = false;
        }
        
    }
}
