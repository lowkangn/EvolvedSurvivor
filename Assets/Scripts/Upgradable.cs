using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public interface Upgradable
    {
        public string GetName();
        public string GetDescription();
        public string GetDetails();
        public Sprite GetSprite();
        public bool IsAbility();
        public bool IsPassiveAbility();

    }
}
