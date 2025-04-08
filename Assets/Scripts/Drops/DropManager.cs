using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Essence essencePrefab;

    private void Awake()
    {
        Enemy.onPassedAway += EnemyPassedAwayCallBack;
    }

    private void OnDestroy()
    {
        Enemy.onPassedAway += EnemyPassedAwayCallBack;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnemyPassedAwayCallBack(Vector2 enemyPosition)
    {
        Instantiate(essencePrefab, enemyPosition, Quaternion.identity, transform);
    }
}
