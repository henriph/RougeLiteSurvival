using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameInput gameInput;
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
}
