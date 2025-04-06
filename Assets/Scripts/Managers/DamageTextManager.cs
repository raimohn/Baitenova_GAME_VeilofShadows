using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private DamageText damageTextPrefab;

    [Header(" Pooling ")]
    private ObjectPool<DamageText> damageTextPool;

    private void Awake()
    {
        Enemy.onDamageTaken += EnemyHitCallback;
    }

    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyHitCallback;
    }

    // Start is called before the first frame update
    void Start()
    {
        damageTextPool = new ObjectPool<DamageText>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    private DamageText CreateFunction()
    {
        return Instantiate(damageTextPrefab, transform);
    }

    private void ActionOnGet(DamageText damageText)
    {
        damageText.gameObject.SetActive(true);
    }

    private void ActionOnRelease(DamageText damageText)
    {
        damageText.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(DamageText damageText)
    {
        Destroy(damageText.gameObject);
    }

    private void EnemyHitCallback(int damage, Vector2 enemyPos, bool isCriticalHit)
    {
        Vector3 spawnPosition = enemyPos + Vector2.up * 0.3f;

        DamageText damageTextInstance = damageTextPool.Get();
        damageTextInstance.transform.position = spawnPosition;

        damageTextInstance.Animate(damage, isCriticalHit);

        LeanTween.delayedCall(1, () => damageTextPool.Release(damageTextInstance));
    }
}
