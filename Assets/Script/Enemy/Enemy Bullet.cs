using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header(" Element ")]
    private Rigidbody2D rig;

    [Header(" Settings ")]
    [SerializeField] float bulletSpeed;
    private float damage;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(int damage, Vector2 direction)
    {
        this.damage = damage;

        transform.right = direction;
        rig.linearVelocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.TryGetComponent(out Player player))
        {
            player.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
