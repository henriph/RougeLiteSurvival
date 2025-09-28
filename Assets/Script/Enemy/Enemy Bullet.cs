using UnityEngine;

[RequireComponent (typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header(" Element ")]
    private Rigidbody2D rig;
    private Collider2D bulletCollider;
    private RangeEnemyAttack rangeEnemyAttack;

    [Header(" Settings ")]
    [SerializeField] float bulletSpeed;
    private int damage;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<Collider2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Configure(RangeEnemyAttack rangeEnemyAttack)
    {
        this.rangeEnemyAttack = rangeEnemyAttack;
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
            player.TakeDamage(damage);
            
            bulletCollider.enabled = false;
            rangeEnemyAttack.ReleaseBullet(this);
        }

        if(collider.CompareTag("Wall"))
        {
            rangeEnemyAttack.ReleaseBullet(this);
        }
    }

    public void Reload()
    {
        rig.linearVelocity = Vector2.zero;
        bulletCollider.enabled = true;
    }
}
