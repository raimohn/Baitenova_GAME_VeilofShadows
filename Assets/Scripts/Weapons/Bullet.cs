using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [Header(" Elements ")]
    private Rigidbody2D rig;
    private Collider2D collider;

    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask enemyMask;
    private int damage;
    private bool isCriticalHit;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        //LeanTween.delayedCall(gameObject, 5, () => rangeEnemyAttack.ReleaseBullet(this));
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(int damage, Vector2 direction, bool isCriticalHit)
    {
        this.damage = damage;
        this.isCriticalHit = isCriticalHit;

        transform.right = direction;
        rig.velocity = direction * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsInLayerMask(collider.gameObject.layer, enemyMask))
        {
            Attack(collider.GetComponent<Enemy>());
            Destroy(gameObject);
        }
    }

    private void Attack(Enemy enemy)
    {
        enemy.TakeDamage(damage, isCriticalHit);
    }
    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask.value & (1 << layer)) != 0;
    }
}
