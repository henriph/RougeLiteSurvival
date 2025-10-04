using TMPro;
using UnityEngine;
using System;
using Unity.VisualScripting;

public abstract class Enemy : MonoBehaviour
{
    [Header(" Components ")]
    protected EnemyMovement movement;

    [Header(" Elements ")]
    protected Player player;

    [Header(" Settings ")]
    [SerializeField] protected float playerDectectionRadius;

    [Header(" Health ")]
    [SerializeField] protected int maxHealth;
    protected int health;
    [SerializeField] protected TextMeshPro healthText;

    [Header(" Actions ")]
    public static Action<Vector2> onPassAway;

    [Header(" Debug ")]
    [SerializeField] protected bool gizmos;


    protected virtual void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        player = FindFirstObjectByType<Player>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        health = maxHealth;
        healthText.text = health.ToString();

        if (player == null)
        {
            Debug.LogError("No player found! Auto destroying object!");
            Destroy(gameObject);
            return;
        }

        movement.StorePlayer(player);
    }

    protected bool IsPlayerInRange()
    {
        if(player == null)
        {
            return false;
        }

        return Vector2.Distance(player.transform.position, transform.position) <= playerDectectionRadius;
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(health, damage);
        health -= realDamage;

        healthText.text = health.ToString();

        if (health <= 0)
        {
            PassAway();
        }
    }

    private void PassAway()
    {
        onPassAway?.Invoke(transform.position);

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (!gizmos)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDectectionRadius);
    }
}
