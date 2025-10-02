using UnityEngine;

public class RangeWeapon : Weapon
{
    [Header(" Elements ")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Bullet bulletPrefab;
    void Start()
    {
        attackDelay = 1f / attackFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        AutoAim();
    }

    private void AutoAim()
    {
        Vector2 targetUpVector = Vector3.up;
        Enemy closestEnemy = ClosestEnemy();

        if (closestEnemy != null)
        {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            
            ManageShooting();
        }
        
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
    }

    private void ManageShooting()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            attackTimer = 0f;
            Shoot();
        }
    }

    private void Shoot()
    {

        //Vector2 direction = (player.GetCenter() - (Vector2)shootingPoint.position).normalized;

        Bullet bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bulletInstance.Shoot(weaponDamage, transform.up);
    }
}
