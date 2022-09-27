using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class TargetDetector : MonoBehaviour
    {
        [SerializeField]
        private float detectionRadius;
        [SerializeField]
        private LayerMask targetLayers;
        [SerializeField]
        private List<string> targetTags;
        [SerializeField]
        private bool onScreen = false;

        public List<Transform> ScanTargets()
        {
            List<Transform> results = new();

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, targetLayers);
            foreach (Collider2D hit in hits)
            {
                if (targetTags.Contains(hit.tag))
                {
                    if (onScreen)
                    {
                        if (GeneralUtility.IsOnScreen(hit.gameObject))
                        {
                            results.Add(hit.transform);
                        }
                    }
                    else
                    {
                        results.Add(hit.transform);
                    }
                }
            }

            results.Sort((a, b) => (a.position - transform.position).sqrMagnitude.CompareTo((b.position - transform.position).sqrMagnitude));

            return results;
        }
    }
}
