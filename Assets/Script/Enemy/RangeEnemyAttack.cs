using System;
using UnityEngine;

public class RangeEnemyAttack : MonoBehaviour
{
    [Header(" Elements ")]
    private Player player;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private EnemyBullet bulletPrefab;
    

    [Header(" Settings ")]
    

    [Header(" Attack ")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    private float attackDelay;
    private float attackTimer;

    [Header(" Debug ")]
    Vector2 gizmosDirection;
    
    void Start()
    {
        attackDelay = 1f / attackFrequency;
        attackTimer = attackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StorePlayer(Player player)
    {
        this.player = player;
    }

    public void AutoAim()
    {
        ManageShooting();
    }

    private void ManageShooting()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay) { 
            attackTimer = 0f;
            Shoot();
        }
    }

    private void Shoot() {
        
        Vector2 direction = (player.GetCenter() - (Vector2)shootingPoint.position).normalized;

        EnemyBullet bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bulletInstance.Shoot(damage, direction);
    }

    
}
