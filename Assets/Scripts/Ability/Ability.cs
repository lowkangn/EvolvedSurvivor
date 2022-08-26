using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

public abstract class Ability : MonoBehaviour
{
    public bool Active { get; protected set; }

    protected abstract void ActivateAbility();
}
