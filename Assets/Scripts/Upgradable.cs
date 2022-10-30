using UnityEngine;

public enum UpgradableAnimatorIndex
{
    EXPLOSIVE = 0,
    ORBITAL = 1,
    ZONAL = 2,
    LOCKON = 3,
    LASER = 4,
    CONE = 5,

    REACT_AR = 6,
    MAX_FP = 7,
    SPEED_TU = 8,
    QUICK_CH = 9,
    CELL_REG = 10,
}

namespace TeamOne.EvolvedSurvivor
{
    public interface Upgradable
    {
        public string GetName();
        public string GetDescription();
        public string GetDetails();
        public Sprite GetSprite();
        public int GetAnimatorIndex();
        public bool IsAbility();
        public bool IsPassiveAbility();

    }
}
