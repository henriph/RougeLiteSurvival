using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private float range;
    [SerializeField] protected LayerMask enemyMask;

    [Header(" Attacks ")]
    [SerializeField] protected int weaponDamage;
    [SerializeField] protected Animator animator;
    [SerializeField] protected float attackFrequency;
    protected float attackDelay;
    protected float attackTimer;

    [Header(" Animations ")]
    [SerializeField] protected float aimLerp;

    [Header(" Debug ")]
    [SerializeField] protected bool gizmos;

    private void Start()
    {
        attackDelay = 1f / attackFrequency;
    }

    private void Update()
    {
        
    }

    protected Enemy ClosestEnemy()
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


    private void OnDrawGizmos()
    {
        if(!gizmos) { return; }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
