using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header(" Elements ")]

    [Header(" Settings ")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyMask;

    [Header(" Debug ")]
    [SerializeField] private bool gizmos;

    private void Start()
    {
        
    }

    private void Update()
    {
        Enemy closestEnemy = null;

        float minDistance = range;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

        if(enemies.Length <= 0)
        {
            transform.up = Vector3.up;
            return;
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

        if (closestEnemy == null) {

            transform.up = Vector3.up;
            return;
        }

        transform.up = (closestEnemy.transform.position - transform.position).normalized;
    }

    private void OnDrawGizmos()
    {
        if(!gizmos) { return; }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
