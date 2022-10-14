using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public interface Upgradable
    {
        public string GetDescription();
        public Sprite GetSprite();
        public bool IsAbility();
        public bool IsPassiveAbility();

    }
}
