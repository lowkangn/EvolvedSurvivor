using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using TeamOne.EvolvedSurvivor;

[AddComponentMenu("Scripts/EnemyAI/Actions/AIActionObstacleAvoidance")]
public class AIActionObstacleAvoidance : AIAction
{
    [SerializeField] private Vector3 offset;

    private CharacterMovement _characterMovement;
    private Vector3 frontDirection;

    [Tooltip("the layer to consider as obstacles (will prevent movement)")]
    public LayerMask ObstaclesLayerMask = LayerManager.ObstaclesLayerMask;

    public override void Initialization()
    {
        if (!ShouldInitialize) return;
        base.Initialization();
        _characterMovement = this.gameObject.GetComponentInParent<Character>()?.FindAbility<CharacterMovement>();
    }

    public override void PerformAction()
    {
        Move();
    }

    protected void Move()
    {
        Vector3 rayCastOriginPoint = this.transform.position + offset;
        frontDirection = Vector3.Normalize(_brain.Target.position - this.transform.position);
        
        Vector3 checkLeft = Quaternion.Euler(0, 0, 35) * frontDirection;
        RaycastHit2D leftHit = MMDebug.RayCast(rayCastOriginPoint, checkLeft, 5f, ObstaclesLayerMask, Color.yellow, true);

        Vector3 checkRight = Quaternion.Euler(0, 0, -35) * frontDirection;
        RaycastHit2D rightHit = MMDebug.RayCast(rayCastOriginPoint, checkRight, 5f, ObstaclesLayerMask, Color.yellow, true);

        if (leftHit.collider == null && rightHit.collider == null)
        {
            NudgeInDirection(GeneralUtility.GenerateRandomChance(0.5f) ? checkLeft : checkRight);
        } 
        else if (leftHit.collider == null)
        {
            NudgeInDirection(checkLeft);
        } 
        else if (rightHit.collider == null)
        {
            NudgeInDirection(checkRight);
        } 
        else
        {
            NudgeInDirection(leftHit.distance >= rightHit.distance ? checkLeft : checkRight);
        }
    }

    private void NudgeInDirection(Vector3 direction)
    {
        _characterMovement.SetHorizontalMovement(direction.x);
        _characterMovement.SetVerticalMovement(direction.y);
    }
}
