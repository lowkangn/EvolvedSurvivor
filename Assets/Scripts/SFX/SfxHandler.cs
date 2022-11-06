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

    protected MMSoundManagerPlayOptions options = MMSoundManagerPlayOptions.Default;

    private int id = 0;

    void Awake()
    {
        InitialiseSfx();
    }

    protected virtual void InitialiseSfx()
    {
        options.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Sfx;
        options.Location = this.transform.position;
        options.Volume = volume;
        options.Loop = loop;

        if (loop)
        {
            this.id = IdAssigner.GetSoundId();
            options.ID = this.id;
        }
    }

    public void PlaySfx()
    {
        StopSfx();
        MMSoundManagerSoundPlayEvent.Trigger(sfx, options);
    }

    public void StopSfx()
    {
        if (id != 0) // id == 0 means ID not assigned, so should not be calling this method (this method is only used for sfx that need to be stopped prematurely and ID must be assigned)
        {
            MMSoundManagerSoundControlEvent.Trigger(MMSoundManagerSoundControlEventTypes.Free, id);
        }     
    }
}
