using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]

public class Bullet : MonoBehaviour
{
    [Header(" Element ")]
    private Rigidbody2D rig;
    private Collider2D bulletCollider;
    private RangeWeapon rangeWeapon;
    private Enemy target;

    [Header(" Settings ")]
    [SerializeField] float bulletSpeed;
    [SerializeField] private LayerMask enemyMask;
    private int damage;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<Collider2D>();
    }

    public void Shoot(int damage, Vector2 direction)
    {
        this.damage = damage;

        transform.right = direction;
        rig.linearVelocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (target != null)
        {
            return;
        }
        if (IsInLayerMask(collider.gameObject.layer, enemyMask))
        {
            target = collider.GetComponent<Enemy>();

            Attack(target);

            bulletCollider.enabled = false;
            Release();

        }
        else if (collider.CompareTag("Wall"))
        {
            Release();
        }
    }


    private void Release()
    {
        if (!gameObject.activeSelf)
            return;

        rangeWeapon.ReleaseBullet(this);
    }

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask.value & (1 << layer)) != 0;
    }

    private void Attack(Enemy enemy) {
        enemy.TakeDamage(damage);
    }

    public void Configure(RangeWeapon rangeWeapon)
    {
        this.rangeWeapon = rangeWeapon;
    }

    public void Reload()
    {
        target = null;

        rig.linearVelocity = Vector2.zero;
        bulletCollider.enabled = true;
    }
}
