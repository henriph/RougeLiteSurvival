using UnityEngine;
using TMPro;

[RequireComponent(typeof(EnemyMovement), typeof(RangeEnemyAttack))]

public class RangeEnemy : Enemy
{
    private RangeEnemyAttack attack;
    
    protected override void Awake()
    {
        base.Awake();
        attack = GetComponent<RangeEnemyAttack>();

        attack.StorePlayer(player);
    }
    protected override void Start()
    {

        base.Start();

        if (player == null)
        {
            Debug.Log("No player found! Auto destroying object");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        ManageAttack();

        transform.localScale = player.transform.position.x > transform.position.x ? Vector3.one : new Vector3(-1, 1, 1);
    }

    private void ManageAttack()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceToPlayer > playerDectectionRadius)
        {
            movement.FollowPlayer();
        }
        else
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        attack.AutoAim();
    }
}
