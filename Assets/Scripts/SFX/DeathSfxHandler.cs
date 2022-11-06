using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

public class DeathSfxHandler : SfxHandler
{
    [SerializeField] Health health;

    void Update()
    {
        if (health.CurrentHealth <= 0)
        {
            PlaySfx();
        }
        this.enabled = false;
    }
}
