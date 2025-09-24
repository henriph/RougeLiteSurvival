using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform hitDetectionTransform;
    [SerializeField] private float hitDectionRadius;

    [Header(" Settings ")]
    [SerializeField] private float range;
    [SerializeField] private int weaponDamage;
    [SerializeField] private LayerMask enemyMask;

    [Header(" Animations ")]
    [SerializeField] private float aimLerp;

    [Header(" Debug ")]
    [SerializeField] private bool gizmos;

    private void Start()
    {
        
    }

    private void Update()
    {
        AutoAim();

        Attack();
    }

    private Enemy ClosestEnemy()
    {
        Enemy closestEnemy = null;

        float minDistance = range;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);
        

        if (enemies.Length <= 0)
        {
            return null;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyChecked = enemies[i].GetComponent<Enemy>();

            if (enemyChecked != null)
            {
                float distance = Vector2.Distance(transform.position, enemyChecked.transform.position);

                if (distance < minDistance)
                {
                    closestEnemy = enemyChecked;
                    minDistance = distance;
                }
            }
        }

        return closestEnemy;
    }
    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(hitDetectionTransform.position, hitDectionRadius, enemyMask);

        for(int i = 0;i < enemies.Length;i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(weaponDamage);
        }
    }
    private void AutoAim()
    {
        Vector2 targetUpVector = Vector3.up;
        Enemy closestEnemy = ClosestEnemy();

        if (closestEnemy != null) {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
        }

        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
    }

    private void OnDrawGizmos()
    {
        if(!gizmos) { return; }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitDetectionTransform.position, hitDectionRadius);
    }
}
