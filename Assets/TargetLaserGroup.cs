using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLaserGroup : MonoBehaviour
{
    [SerializeField] private List<TargetingLaser> lasers;
    [SerializeField] private float fireAfterSeconds;

    private void Start()
    {
        InvokeRepeating("WarmUpLasers", 0f, 10f);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void WarmUpLasers()
    {
        StartCoroutine(WarmUpLaserCoroutine());
    }

    private IEnumerator WarmUpLaserCoroutine()
    {
        yield return new WaitForSeconds(fireAfterSeconds);

        SetLasers(true);

        yield return new WaitForSeconds(1f);

        ArmLasers();
    }

    private void SetLasers(bool isActive)
    {
        foreach (TargetingLaser laser in lasers)
        {
            laser.gameObject.SetActive(isActive);
        }
    }

    private void ArmLasers()
    {
        foreach (TargetingLaser laser in lasers)
        {
            // Give at least 0.5f for the lasers to converge and stay on target
            laser.ArmLaser(4f);
        }
    }
}
