using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class UiSfxHandler : SfxHandler
{
    protected override void InitialiseSfx()
    {
        base.InitialiseSfx();
        options.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.UI;

    }
}
