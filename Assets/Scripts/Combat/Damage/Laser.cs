using System.Linq;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class Laser : RecursableDamageArea
    {
        [SerializeField]
        private float length = 15f;

        private LineRenderer lineRenderer;
        private BoxCollider2D lineCollider;

        private Transform startPoint;
        private Vector3 direction;
        private float width;

        public void InitializeLaser(Transform startPoint, Vector3 direction, float width)
        {
            lineRenderer ??= GetComponent<LineRenderer>();
            lineCollider ??= GetComponent<BoxCollider2D>();

            this.startPoint = startPoint;
            this.direction = direction;
            this.width = width;
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
            RenderLaser();
        }

        private void RenderLaser()
        {
            lineRenderer.SetPosition(0, startPoint.position);
            Vector3 endPoint = startPoint.position + direction * length;
            lineRenderer.SetPosition(1, endPoint);

            transform.position = startPoint.position;
            transform.up = direction;

            lineCollider.size = new Vector2(width, length);
            lineCollider.offset = new Vector2(0, length / 2);
        }

        protected override void Update()
        {
            base.Update();
            RenderLaser();
        }

        protected override void SpawnRecursiveAbility()
        {
            if (recursiveAbility != null && !wasRecursiveUsed)
            {
                foreach (GameObject target in targetsHit.ToList())
                {
                    recursiveAbility.SetActive(true);
                    recursiveAbility.transform.position = target.transform.position;
                    wasRecursiveUsed = true;
                }
            }    
        }
    }
}
