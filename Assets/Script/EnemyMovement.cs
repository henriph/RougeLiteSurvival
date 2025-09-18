using System.Threading;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header(" Elements ")]
    private Player player;

    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;


    public void StorePlayer(Player player)
    {
        this.player = player;
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        Vector2 targetDirection = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetDirection;
    }
}
