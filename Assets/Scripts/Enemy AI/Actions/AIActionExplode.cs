using MoreMountains.Tools;
using System.Collections;
using UnityEngine;

/// <summary>
/// An AIAction used to cause the enemy to explode.
/// </summary>
[AddComponentMenu("Scripts/EnemyAI/Actions/AIActionExplode")]
public class AIActionExplode : AIAction
{
    private const string explodeParameterName = "Explode";

    [SerializeField] private Animator animator;
    [SerializeField] private float explosionDuration = 1.0f;

    [SerializeField] private GameObject explosion;

    public bool OnlyRunOnce = true;

    protected bool _alreadyRan = false;

    public override void PerformAction()
    {
        if (OnlyRunOnce && !_alreadyRan)
        {
            MMAnimatorExtensions.UpdateAnimatorBoolIfExists(animator, explodeParameterName, true);

            _alreadyRan = true;
            _brain.BrainActive = false;

            StartCoroutine("DamagePlayerIfClose");
            Invoke("DestroyObject", explosionDuration);
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

    private IEnumerator DamagePlayerIfClose()
    {
        yield return new WaitForSeconds(explosionDuration / 2);

        explosion.SetActive(true);
        
        yield return new WaitForSeconds(0.1f);

        explosion.SetActive(false);
    }

    private void DestroyObject()
    {
        gameObject.SetActive(false);
    }
}
