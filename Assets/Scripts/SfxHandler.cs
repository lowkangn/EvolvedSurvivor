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
    }

    public void PlaySfx()
    {
        MMSoundManagerSoundPlayEvent.Trigger(sfx, options);
    }
}
