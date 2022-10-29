using MoreMountains.Feedbacks;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using TeamOne.EvolvedSurvivor;
using UnityEngine;

public class TargetLaserGroup : MonoBehaviour
{
    [SerializeField] private List<TargetingLaser> lasers;
    [SerializeField] private float fireAfterSeconds;
    [SerializeField] private float fireDuration = 4f;
    [SerializeField] private ParticleSystem explosionParticles;

    [SerializeField] private SpawnManagerScriptableObject spawnManager;
    [SerializeField] private float damageValue = 1000f;

    private Health playerHealth;

    private void Start()
    {
        InvokeRepeating("WarmUpLasers", 0f, 8f);
    }

    private void OnEnable()
    {
        spawnManager.PlayerSpawnEvent.AddListener(AttachToPlayer);
    }

    private void OnDisable()
    {
        spawnManager.PlayerSpawnEvent.RemoveListener(AttachToPlayer);
        StopAllCoroutines();
    }

    private void AttachToPlayer(PlayerController player)
    {
        gameObject.transform.position = player.transform.position;
        gameObject.transform.parent = player.transform;

        playerHealth = player.GetComponent<Health>();
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

        yield return new WaitForSeconds(fireDuration);
        Fire();
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
            laser.ArmLaser(fireDuration);
        }
    }

    private void Fire()
    {
        MMFlashEvent.Trigger(Color.red, 0.5f, 1f, 0, 0, TimescaleModes.Unscaled);
        explosionParticles.gameObject.SetActive(true);
        explosionParticles.Play();
        playerHealth.Damage(damageValue, gameObject, 0f, 0f, Vector3.zero);
    }
}
