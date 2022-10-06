using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using TeamOne.EvolvedSurvivor;

[AddComponentMenu("TopDown Engine/Character/AI/Actions/AIActionMoveTowardsTargetWithObstacleAvoidance")]
public class AIActionMoveTowardsTargetWithObstacleAvoidance : AIActionMoveTowardsTarget2D
{
    [SerializeField] private GameObject rayCastOrigin;

    [Tooltip("the layer to consider as obstacles (will prevent movement)")]
    public LayerMask ObstaclesLayerMask = LayerManager.ObstaclesLayerMask;

    protected override void Move()
    {
        base.Move();

        Vector3 rayCastOriginPoint = rayCastOrigin.transform.position;
        Vector3 frontDirection = Vector3.Normalize(_brain.Target.position - this.transform.position);
        RaycastHit2D hit = MMDebug.BoxCast(rayCastOriginPoint, new Vector2(1f, 1f), 90f, frontDirection, 1.5f, ObstaclesLayerMask, Color.yellow, true);
        
        if (hit.collider != null)
        {
            Vector3 checkLeft = Quaternion.Euler(0, 0, -45) * frontDirection;
            RaycastHit2D leftHit = MMDebug.RayCast(rayCastOriginPoint, frontDirection, 2f, ObstaclesLayerMask, Color.yellow, true);

            Vector3 checkRight = Quaternion.Euler(0, 0, 45) * frontDirection;
            RaycastHit2D rightHit = MMDebug.RayCast(rayCastOriginPoint, frontDirection, 2f, ObstaclesLayerMask, Color.yellow, true);

            if (checkLeft == null && checkRight == null)
            {
                NudgeInDirection(GeneralUtility.GenerateRandomChance(0.5f) ? checkLeft : checkRight);
            } 
            else if (checkLeft == null)
            {
                NudgeInDirection(checkLeft);
            } 
            else if (checkRight == null)
            {
                NudgeInDirection(checkRight);
            } 
            else
            {
                NudgeInDirection(leftHit.distance >= rightHit.distance ? checkLeft : checkRight);
            }
        }
    }

    private void NudgeInDirection(Vector3 direction)
    {
        _characterMovement.SetHorizontalMovement(direction.x > 0f ? 1f : -1f);
        _characterMovement.SetVerticalMovement(direction.y > 0f ? 1f : -1f);
    }
}
