using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]

public class Bullet : MonoBehaviour
{
    [Header(" Element ")]
    private Rigidbody2D rig;
    private Collider2D bulletCollider;

    [Header(" Settings ")]
    [SerializeField] float bulletSpeed;
    [SerializeField] private LayerMask enemyMask;
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

    public void Shoot(int damage, Vector2 direction)
    {
        this.damage = damage;

        transform.right = direction;
        rig.linearVelocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsInLayerMask(collider.gameObject.layer, enemyMask))
        {
            Attack(collider.GetComponent<Enemy>());
            Destroy(gameObject);
        }
    }

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask.value & (1 << layer)) != 0;
    }

    private void Attack(Enemy enemy) {
        enemy.TakeDamage(damage);
    }
}
