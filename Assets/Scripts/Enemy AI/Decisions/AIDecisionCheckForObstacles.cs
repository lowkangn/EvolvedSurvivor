using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

[AddComponentMenu("Scripts/EnemyAI/Decisions/AIDecisionCheckForObstacles")]
public class AIDecisionCheckForObstacles : AIDecision
{
    [SerializeField] private Vector3 offset;

    [Tooltip("the layer to consider as obstacles (will prevent movement)")]
    public LayerMask ObstaclesLayerMask = LayerManager.ObstaclesLayerMask;

    private float timeSinceLastAdjustment = 0f;

    private void Update()
    {
        if (timeSinceLastAdjustment > 0f)
        {
            timeSinceLastAdjustment -= Time.deltaTime;
        }
    }

    public override bool Decide()
    {
        if (timeSinceLastAdjustment > 0f)
        {
            return true;
        }

        Vector3 rayCastOriginPoint = this.transform.position + offset;
        Vector3 frontDirection = Vector3.Normalize(_brain.Target.position - this.transform.position);
        RaycastHit2D hit = MMDebug.BoxCast(rayCastOriginPoint, new Vector2(1f, 1f), 0f, frontDirection, 3f, ObstaclesLayerMask, Color.yellow, true);

        if (hit.collider != null)
        {
            timeSinceLastAdjustment = 1f;
        }

        return hit.collider != null;
    }
}
