using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DropManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Essence essencePrefab;
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private Chest chestPrefab;

    [Header(" Settings ")]
    [SerializeField] [Range(0, 100)] private int cashDropChance;
    [SerializeField] [Range(0, 100)] private int chestDropChance;

    [Header (" Pooling ")]
    private ObjectPool<Essence> essencePool;
    private ObjectPool<Coin> coinPool;

    private void Awake()
    {
        Enemy.onPassedAway  += EnemyPassedAwayCallBack;
        Essence.onCollected += ReleaseEssence;
        Coin.onCollected    += ReleaseCoin;
    }

    private void OnDestroy()
    {
        Enemy.onPassedAway  -= EnemyPassedAwayCallBack;
        Essence.onCollected -= ReleaseEssence;
        Coin.onCollected    -= ReleaseCoin;
    }


    // Start is called before the first frame update
    void Start()
    {
        essencePool = new ObjectPool<Essence>(
            EssenceCreateFunction, 
            EssenceActionOnGet, 
            EssenceActionOnRelease, 
            EssenceActionOnDestroy);

        coinPool = new ObjectPool<Coin>(
            CoinCreateFunction, 
            CoinActionOnGet, 
            CoinActionOnRelease, 
            CoinActionOnDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Essence EssenceCreateFunction()              => Instantiate(essencePrefab, transform);
    private void EssenceActionOnGet(Essence essence)     => essence.gameObject.SetActive(true);
    private void EssenceActionOnRelease(Essence essence) => essence.gameObject.SetActive(false);
    private void EssenceActionOnDestroy(Essence essence) => Destroy(essence.gameObject);

    private Coin CoinCreateFunction()                    => Instantiate(coinPrefab, transform);
    private void CoinActionOnGet(Coin coin)              => coin.gameObject.SetActive(true);
    private void CoinActionOnRelease(Coin coin)          => coin.gameObject.SetActive(false);
    private void CoinActionOnDestroy(Coin coin)          => Destroy(coin.gameObject);

    private void EnemyPassedAwayCallBack(Vector2 enemyPosition)
    {
        bool shouldSpawnCoin = UnityEngine.Random.Range(0, 101) <= cashDropChance;

        DroppableCurrency droppable = shouldSpawnCoin ? coinPool.Get() : essencePool.Get();
        droppable.transform.position = enemyPosition;

        TryDropChest(enemyPosition);
    }

    private void TryDropChest(Vector2 spawnPosition)
    {
        bool shouldSpawnChest = UnityEngine.Random.Range(0, 101) <= chestDropChance;

        if (!shouldSpawnChest)
            return;

        Instantiate(chestPrefab, spawnPosition, Quaternion.identity, transform);
    }

    private void ReleaseEssence(Essence essence) => essencePool.Release(essence);
    private void ReleaseCoin(Coin coin)          => coinPool.Release(coin);
}
