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
        EnemyProjectile nextProjectile = objectPool
                .GetPooledGameObject().GetComponent<EnemyProjectile>();

        nextProjectile.transform.position = transform.position;
        nextProjectile.SetDamage(damage);
        nextProjectile.SetSpeed(projectileSpeed);

        Vector3 directionToTarget = Vector3
            .Normalize(_brain.Target.position - transform.position);
        nextProjectile.SetDirection(directionToTarget);

        nextProjectile.gameObject.SetActive(true);
    }
}
