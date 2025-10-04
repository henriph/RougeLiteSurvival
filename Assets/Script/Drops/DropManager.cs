using System;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Candy candyPrefab;

    private void Awake()
    {
        Enemy.onPassAway += EnemyPassAwayCallBack;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Enemy.onPassAway -= EnemyPassAwayCallBack;
    }
    private void EnemyPassAwayCallBack(Vector2 enemyPosition)
    {
        Instantiate(candyPrefab, enemyPosition, Quaternion.identity, transform);
    }
}
