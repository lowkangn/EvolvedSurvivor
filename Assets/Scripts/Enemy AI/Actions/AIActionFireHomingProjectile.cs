using MoreMountains.Tools;
using TeamOne.EvolvedSurvivor;
using UnityEngine;

public class AIActionFireHomingProjectile : AIAction
{
    [SerializeField] private MMSimpleObjectPooler projectilePool;
    [SerializeField] private float timeForEachShot = 3f;

    [SerializeField] private SecondaryAttackEnemy enemy;
    [SerializeField] private float projectileSpeed = 8f;

    private float timeSinceLastShot = 0f;
    private Damage damage;

    private void Start()
    {
        damage = new Damage();
        damage.damage = enemy.SecondaryDamage;
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
        HomingProjectile nextProjectile = projectilePool
                .GetPooledGameObject().GetComponent<HomingProjectile>();

        nextProjectile.transform.position = transform.position;
        nextProjectile.SetDamage(damage);

        Vector3 directionToTarget = Vector3
            .Normalize(_brain.Target.position - transform.position);
        Vector3 projectileMotion = directionToTarget * projectileSpeed;
        nextProjectile.SetMotion(projectileMotion);

        nextProjectile.SetTarget(ESLevelManager.Instance.Players[0].gameObject);

        nextProjectile.gameObject.SetActive(true);
    }
}
