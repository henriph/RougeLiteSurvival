using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header(" Element ")]
    private Player player;

    [Header(" Setting ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDectectionRadius;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();

        if(player == null)
        {
            Debug.Log("No player found! Auto destroying object");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        FollowPlayer();
        TryAttack();
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        Vector2 targetDirection = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetDirection;
    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= playerDectectionRadius) {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDectectionRadius);
    }
}
