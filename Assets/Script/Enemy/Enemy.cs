using UnityEngine;
using TMPro;

[RequireComponent (typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [Header(" Components ")]
    private EnemyMovement movement;

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
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    private float attackDelay;
    private float attackTimer;

    [Header(" Debug ")]
    [SerializeField] private bool gizmos;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        player = FindFirstObjectByType<Player>();
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

        attackDelay = 1f / attackFrequency;
        movement.StorePlayer(player);
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

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(health, damage);
        health -= realDamage;

        healthText.text = health.ToString();

        Debug.Log("Enemy took " + realDamage);

        if(health <= 0)
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
