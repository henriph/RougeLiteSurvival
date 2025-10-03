using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    enum State
    {
        Idle,
        Attack
    }

    private State state;

    [Header(" Settings ")]


    [Header(" Elements ")]
    [SerializeField] private Transform hitDetectionTransform;
    [SerializeField] private BoxCollider2D hitCollider;

    [Header(" Attacks ")]
    private List<Enemy> damageEnemies = new List<Enemy>();

    void Start()
    {
        state = State.Idle;
    }

    
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                AutoAim();
                break;

            case State.Attack:
                Attacking();
                break;
        }
    }

    private void AutoAim()
    {
        Vector2 targetUpVector = Vector3.up;
        Enemy closestEnemy = ClosestEnemy();

        if (closestEnemy != null)
        {

            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            transform.up = targetUpVector;
            ManageAttackTimer();
        }

        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);

        IncrementTimer();
    }


    private void StartAttack()
    {
        float animationSpeedMultiplier = attackFrequency;

        // Set the animator speed to match the frequency
        animator.speed = animationSpeedMultiplier;

        animator.Play("Attack");
        state = State.Attack;

        damageEnemies.Clear();
    }
    private void Attacking()
    {
        Attack();
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll
            (
            hitDetectionTransform.position,
            hitCollider.bounds.size,
            hitDetectionTransform.localEulerAngles.z,
            enemyMask
            );

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();
  
            if (!damageEnemies.Contains(enemy))
            {
                enemy.TakeDamage(weaponDamage);
                damageEnemies.Add(enemy);
            }
        }
    }

    private void StopAttack()
    {
        state = State.Idle;
        //clear the Damage enemy list
        damageEnemies.Clear();
    }

    private void ManageAttackTimer()
    {
        if (attackTimer >= attackDelay)
        {
            attackTimer = 0f;
            StartAttack();
        }
    }

    private void IncrementTimer()
    {
        attackTimer += Time.deltaTime;
    }
}


