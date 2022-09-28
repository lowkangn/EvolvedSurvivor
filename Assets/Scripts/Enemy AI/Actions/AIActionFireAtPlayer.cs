using MoreMountains.Tools;
using TeamOne.EvolvedSurvivor;
using UnityEngine;

public class AIActionFireAtPlayer : AIAction
{
    [SerializeField] private MMSimpleObjectPooler objectPool;
    [SerializeField] private float timeForEachShot = 3f;

    [SerializeField] private float damageValue = 10f;
    [SerializeField] private float projectileSpeed = 10f;

    private float timeSinceLastShot = 0f;
    private Damage damage;

    private void Start()
    {
        damage = new Damage();
        damage.damage = damageValue;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    public override void PerformAction()
    {
        if (timeSinceLastShot >= timeForEachShot)
        {
            timeSinceLastShot = 0f;

            FireProjectile();
        }
    }
    
    private void FireProjectile()
    {
        Projectile nextProjectile = objectPool
                .GetPooledGameObject().GetComponent<Projectile>();

        nextProjectile.transform.position = transform.position;
        nextProjectile.SetDamage(damage);

        Vector3 directionToTarget = Vector3
            .Normalize(_brain.Target.position - transform.position);
        Vector3 projectileMotion = directionToTarget * projectileSpeed;
        nextProjectile.SetMotion(projectileMotion);

        nextProjectile.gameObject.SetActive(true);
    }
}
