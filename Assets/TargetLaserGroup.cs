using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLaserGroup : MonoBehaviour
{
    [SerializeField] private List<TargetingLaser> lasers;
    [SerializeField] private float fireAfterSeconds;

    private void Start()
    {
        InvokeRepeating("WarmUpLasers", 0f, 8f);
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
        yield return new WaitForSeconds(0.1f);
        SetLasers(false);
        yield return new WaitForSeconds(0.1f);
        SetLasers(true);
        yield return new WaitForSeconds(0.2f);
        SetLasers(false);
        yield return new WaitForSeconds(0.2f);
        SetLasers(true);

        yield return new WaitForSeconds(0.3f);

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
