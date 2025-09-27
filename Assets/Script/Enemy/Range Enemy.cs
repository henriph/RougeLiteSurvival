using UnityEngine;
using TMPro;

[RequireComponent(typeof(EnemyMovement), typeof(RangeEnemyAttack))]

public class RangeEnemy : MonoBehaviour
{
    [Header(" Components ")]
    private EnemyMovement movement;
    private RangeEnemyAttack attack;

    [Header(" Elements ")]
    private Player player;

    [Header(" Settings ")]
    [SerializeField] private float playerDectectionRadius;


    [Header(" Health ")]
    [SerializeField] private int maxHealth;
    private int health;
    [SerializeField] private TextMeshPro healthText;

    [Header(" Effects")]

    [Header(" Attack ")]
    

    [Header(" Debug ")]
    [SerializeField] private bool gizmos;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<RangeEnemyAttack>();
        player = FindFirstObjectByType<Player>();

        attack.StorePlayer(player);
    }
    private void Start()
    {
        health = maxHealth;
        healthText.text = health.ToString();

        if (player == null)
        {
            Debug.Log("No player found! Auto destroying object");
            Destroy(gameObject);
        }

        movement.StorePlayer(player);
    }

    private void Update()
    {
        ManageAttack();

        
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

    

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(health, damage);
        health -= realDamage;

        healthText.text = health.ToString();

        Debug.Log("Enemy took " + realDamage);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnDrawGizmos()
    {
        if (!gizmos)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDectectionRadius);
    }
}
