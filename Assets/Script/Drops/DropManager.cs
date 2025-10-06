using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class DropManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Candy candyPrefab;
    [SerializeField] private Cash cashPrefab;

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
        bool shouldSpawnCash = Random.Range(0, 101) <= 20;

        GameObject droppable = shouldSpawnCash ? cashPrefab.gameObject : candyPrefab.gameObject;

        GameObject droppableInstance = Instantiate(droppable, enemyPosition, Quaternion.identity, transform);
    }
}
