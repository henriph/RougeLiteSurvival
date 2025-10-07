using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

using Random = UnityEngine.Random;

public class DropManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Candy candyPrefab;
    [SerializeField] private Cash cashPrefab;
    [SerializeField] private Chest chestPrefab;

    [Header(" Settings ")]
    [SerializeField] [Range(0, 100)] private float cashDropRate;
    [SerializeField] [Range(0, 100)] private float chestDropRate;

    [Header(" Drops Pooling ")]
    private ObjectPool<Candy> candyPool;
    private ObjectPool<Cash> cashPool;


    private void Awake()
    {
        Enemy.onPassAway += EnemyPassAwayCallBack;
        Candy.onCollected += ReleaseCandy;
        Cash.onCollected += ReleaseCash;
    }

    void OnDestroy()
    {
        Enemy.onPassAway -= EnemyPassAwayCallBack;
        Candy.onCollected -= ReleaseCandy;
        Cash.onCollected -= ReleaseCash;
    }

    void Start()
    {
        candyPool = new ObjectPool<Candy>(
            CandyCreateFunction, 
            CandyActionOnGet, 
            CandyActionOnRelease, 
            CandyActionOnDestroy);

        cashPool = new ObjectPool<Cash>(
            CashCreateFunction,
            CashActionOnGet,
            CashActionOnRelease,
            CashActionOnDestroy);
    }


    private Candy CandyCreateFunction()             => Instantiate(candyPrefab, transform);
    private void CandyActionOnGet(Candy candy)      => candy.gameObject.SetActive(true);
    private void CandyActionOnRelease(Candy candy)  => candy.gameObject.SetActive(false);
    private void CandyActionOnDestroy(Candy candy)  => Destroy(candy.gameObject);


    private Cash CashCreateFunction()               => Instantiate(cashPrefab, transform);
    private void CashActionOnGet(Cash cash)         => cash.gameObject.SetActive(true);
    private void CashActionOnRelease(Cash cash)     => cash.gameObject.SetActive(false);
    private void CashActionOnDestroy(Cash cash)     => Destroy(cash.gameObject);
    
    private void EnemyPassAwayCallBack(Vector2 enemyPosition)
    {
        bool shouldSpawnCash = Random.Range(0, 101) <= cashDropRate;

        Droppable droppable = shouldSpawnCash ? cashPool.Get() : candyPool.Get();
        droppable.transform.position = enemyPosition;

        TryDropChest(enemyPosition);
    }

    private void TryDropChest(Vector2 enemyposition)
    {
        bool shouldSpawnChest = Random.Range(0, 101) <= chestDropRate;

        if (!shouldSpawnChest)
            return;

        Instantiate(chestPrefab, enemyposition, Quaternion.identity, transform);
    }

    private void ReleaseCandy(Candy candy) => candyPool.Release(candy);
    private void ReleaseCash(Cash cash) => cashPool.Release(cash);
}
