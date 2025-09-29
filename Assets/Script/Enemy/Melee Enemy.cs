using UnityEngine;
using TMPro;

[RequireComponent (typeof(EnemyMovement))]
public class MeleeEnemy : Enemy
{

    [Header(" Effects")]

    [Header(" Attack ")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    private float attackDelay;
    private float attackTimer;

    protected override void Start()
    {
        base.Start();

        attackDelay = 1f / attackFrequency;
    }

    private void Update()
    {
        if (attackTimer >= attackDelay)
        {
            TryAttack();
            attackTimer = 0f;
        }
        else
        {
            Wait();
        }

        movement.FollowPlayer();
    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= playerDectectionRadius)
        {
            Attack();
        }
    }

    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }

    private void Attack()
    {
        Debug.Log("Dealing " + damage + " to player");
        player.TakeDamage(damage);
    }



}
