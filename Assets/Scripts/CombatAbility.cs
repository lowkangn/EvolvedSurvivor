using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

public class CombatAbility : MonoBehaviour
{
    [SerializeField] private float abilityCooldown;
    [SerializeField] private MMObjectPooler objectPool;
    [SerializeField] private bool isLockOn;
    [SerializeField] private bool isValidAbility; 

    private float remainingCooldown;

    private void Start()
    {
        remainingCooldown = abilityCooldown;
    }

    private void Update()
    {
        FireAbility();
    }
    
    private void FireAbility()
    {
        if (!isValidAbility)
        {
            return;
        } 
        else
        {
            remainingCooldown -= Time.deltaTime;
        }

        if (remainingCooldown <= 0f)
        {
            GameObject nextGameObject = objectPool.GetPooledGameObject();
            nextGameObject.transform.position = gameObject.transform.position;
            nextGameObject.SetActive(true);

            Projectile projectile = nextGameObject.GetComponent<Projectile>();
            SetRandomDirection(projectile);

            remainingCooldown = abilityCooldown;
        }
    }

    private void SetRandomDirection(Projectile projectile)
    {
        projectile.SetDirection(
            new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f),
            transform.rotation);
    }
}
