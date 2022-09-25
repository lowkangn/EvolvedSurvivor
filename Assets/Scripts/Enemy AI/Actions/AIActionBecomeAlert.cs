using MoreMountains.Tools;
using UnityEngine;

/// <summary>
/// An AIAction used to display an alert animation.
/// </summary>
[AddComponentMenu("Scripts/EnemyAI/Actions/AIActionBecomeAlert")]
public class AIActionBecomeAlert : AIAction
{
    [SerializeField] private Animator animator;

    public bool OnlyRunOnce = true;

    protected bool _alreadyRan = false;

    private const string alertParameterName = "Alert";

    public override void PerformAction()
    {
        if (OnlyRunOnce && !_alreadyRan)
        {
            MMAnimatorExtensions.UpdateAnimatorBoolIfExists(animator, alertParameterName, true);
            _alreadyRan = true;
        }
    }

    /// <summary>
    /// On enter state we reset our flag
    /// </summary>
    public override void OnEnterState()
    {
        base.OnEnterState();
        _alreadyRan = false;
    }

    /// <summary>
    /// On exit state we exit alert animation state
    /// </summary>
    public override void OnExitState()
    {
        base.OnExitState();
        MMAnimatorExtensions.UpdateAnimatorBoolIfExists(animator, alertParameterName, false);
    }
}
