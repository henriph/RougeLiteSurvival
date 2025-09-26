using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    enum State
    {
        Idle,
        Attack
    }

    private State state;

    [Header(" Elements ")]
    [SerializeField] private Transform hitDetectionTransform;
    [SerializeField] private float hitDectionRadius;

    [Header(" Settings ")]
    [SerializeField] private float range;
    
    [SerializeField] private LayerMask enemyMask;

    [Header(" Attacks ")]
    [SerializeField] private int weaponDamage;
    [SerializeField] private Animator animator;
    [SerializeField] private float attackFrequency;
    private float attackDelay;
    private float attackTimer;
    private List<Enemy> damageEnemies = new List<Enemy>();

    [Header(" Animations ")]
    [SerializeField] private float aimLerp;

    [Header(" Debug ")]
    [SerializeField] private bool gizmos;

    private void Start()
    {
        attackDelay = 1f / attackFrequency;
        state = State.Idle;
    }

    

    private void Update()
    {
        switch(state)
        {
            case State.Idle:
                AutoAim();
                break;

            case State.Attack:
                Attacking();
                break;
        }
    }

    private Enemy ClosestEnemy()
    {
        Enemy closestEnemy = null;

        float minDistance = range;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);
        

        if (enemies.Length <= 0)
        {
            return null;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyChecked = enemies[i].GetComponent<Enemy>();

            if (enemyChecked != null)
            {
                float distance = Vector2.Distance(transform.position, enemyChecked.transform.position);

                if (distance < minDistance)
                {
                    closestEnemy = enemyChecked;
                    minDistance = distance;
                }
            }
        }

        return closestEnemy;
    }
    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(hitDetectionTransform.position, hitDectionRadius, enemyMask);

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            //1. is the enemy inside the list ?
            //2. if no, attack the enemy and put it into the list
            //3. if yes, continue, check the next enemy
            if (!damageEnemies.Contains(enemy)) {
                enemy.TakeDamage(weaponDamage);
                damageEnemies.Add(enemy);
            }
        }
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

    private void ManageAttackTimer()
    {
        if (attackTimer >= attackDelay)
        {
            attackTimer = 0f;
            StartAttack();
        }
    }

    private void StopAttack()
    {
        state = State.Idle;
        //clear the Damage enemy list
        damageEnemies.Clear();
    }
    private void AutoAim()
    {
        Vector2 targetUpVector = Vector3.up;
        Enemy closestEnemy = ClosestEnemy();

        if (closestEnemy != null) {
            
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            transform.up = targetUpVector;
            ManageAttackTimer();
        }

        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);

        IncrementTimer();
    }

    private void IncrementTimer()
    {
        attackTimer += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        if(!gizmos) { return; }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitDetectionTransform.position, hitDectionRadius);
    }
}
