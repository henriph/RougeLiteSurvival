using UnityEngine;


public class Player : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private int playerHealth;

    [Header(" Elements ")]
    [SerializeField] GameInput gameInput;

    [Header(" Settings ")]
    [SerializeField] float moveSpeed = 6;
    Rigidbody2D rb;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
         playerHealth -= damage;
    }
}
