using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAttract : MonoBehaviour
{
    [SerializeField] float pickupSpeed;
    private Transform playerTransform;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("ESPickup"))
        {
            return;
        }

        this.playerTransform = collider.gameObject.transform.parent;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * pickupSpeed;
        }
    }
}
