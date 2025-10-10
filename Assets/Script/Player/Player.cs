using UnityEngine;

[RequireComponent (typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    public static Player instance;

    [Header(" Components ")]
    Rigidbody2D rb;
    private PlayerHealth playerHealth;
    private CircleCollider2D playerCollider;
    private PlayerLevel playerLevel;

    [Header(" Elements ")]
    [SerializeField] GameInput gameInput;

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
        playerCollider = GetComponent<CircleCollider2D>();
        playerLevel = GetComponent<PlayerLevel>();
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

    public bool HasLeveledUp()
    {
        return playerLevel.HasLeveledUp();
    }

}
