using UnityEngine;

[RequireComponent (typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [Header(" Components ")]
    Rigidbody2D rb;
    private PlayerHealth playerHealth;
    private CircleCollider2D playerCollider;

    [Header(" Elements ")]
    [SerializeField] GameInput gameInput;

    [Header(" Settings ")]
    [SerializeField] float moveSpeed = 6;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
       
    }

    private void FixedUpdate()
    {

        rb.linearVelocity = gameInput.InputMovementNormalized * moveSpeed;
    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }

    public Vector2 GetCenter()
    {
        return playerCollider.bounds.center;
    }

    
}
