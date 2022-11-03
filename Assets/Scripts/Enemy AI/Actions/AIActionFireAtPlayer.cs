using MoreMountains.Tools;
using TeamOne.EvolvedSurvivor;
using UnityEngine;

public class AIActionFireAtPlayer : AIAction
{
    [SerializeField] private MMSimpleObjectPooler projectilePool;
    [SerializeField] private float timeForEachShot = 3f;

    [SerializeField] private SecondaryAttackEnemy enemy;
    [SerializeField] private float projectileSpeed = 10f;

    [SerializeField] private SfxHandler sfxHandler;

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
        Projectile nextProjectile = projectilePool
                .GetPooledGameObject().GetComponent<Projectile>();

        nextProjectile.transform.position = transform.position;
        nextProjectile.SetDamage(damage);

        Vector3 directionToTarget = Vector3
            .Normalize(_brain.Target.position - transform.position);
        Vector3 projectileMotion = directionToTarget * projectileSpeed;
        nextProjectile.SetMotion(projectileMotion);

        nextProjectile.gameObject.SetActive(true);

        sfxHandler.PlaySfx();
    }
}
