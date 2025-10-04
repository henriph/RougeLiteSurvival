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
            Release();


        } else if(collider.CompareTag("Wall"))
        {
            Release();
        }
    }

    private void Release()
    {
        if(!gameObject.activeSelf)
        {
            return;
        }

        rangeEnemyAttack.ReleaseBullet(this);
    }

    public void Reload()
    {
        rig.linearVelocity = Vector2.zero;
        bulletCollider.enabled = true;
    }
}
