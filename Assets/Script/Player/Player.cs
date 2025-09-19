using UnityEngine;

[RequireComponent (typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [Header(" Components ")]
    Rigidbody2D rb;
    private PlayerHealth playerHealth;
    

    [Header(" Elements ")]
    [SerializeField] GameInput gameInput;

    [Header(" Settings ")]
    [SerializeField] float moveSpeed = 6;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
       
    }

    private void Update()
    {
        Vector2 inputMovement = gameInput.InputMovementNormalized;
    }

    private void FixedUpdate()
    {

        rb.linearVelocity = gameInput.InputMovementNormalized * moveSpeed;
    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }

    
}
