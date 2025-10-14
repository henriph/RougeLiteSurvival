using UnityEngine;

[RequireComponent (typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    public static Player instance;

    [Header(" Components ")]
    Rigidbody2D rb;
    private PlayerHealth playerHealth;
    private BoxCollider2D playerCollider;
    private PlayerLevel playerLevel;

    [Header(" Elements ")]
    [SerializeField] GameInput gameInput;
    [SerializeField] private Animator animator;

    [Header(" Settings ")]
    [SerializeField] float moveSpeed = 6;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerLevel = GetComponent<PlayerLevel>();
    }
    private void Start()
    {
       
    }

    private void FixedUpdate()
    {
        Vector2 inputMovement = gameInput.InputMovementNormalized;
        rb.linearVelocity = inputMovement * moveSpeed;

        HandleMovement(inputMovement);
        
    }

    private void HandleMovement(Vector2 inputMovement)
    {
        IsRunning(inputMovement);
        FlipCharacterX(inputMovement);
        
    }
    private void FlipCharacterX(Vector2 inputMovement)
    {
        if (inputMovement.x > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputMovement.x < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void IsRunning(Vector2 inputMovement)
    {
        if(inputMovement != Vector2.zero)
        {
            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }

    public Vector2 GetCenter()
    {
        return playerCollider.bounds.center;
    }

    public bool HasLeveledUp()
    {
        return playerLevel.HasLeveledUp();
    }

}
