using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [Header(" Components ")]
    private EnemyMovement movement;
    private Animator animator;

    [Header(" Elements ")]
    [SerializeField] Player player;

    [Header(" Health ")]
    [SerializeField] private int maxHealth;
    private int health;
    [SerializeField] private TextMeshPro healthText;

    [Header(" Spawn Sequence Related ")]
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    [SerializeField] private Collider2D enemyCollider;
    private bool hasSpawned;

    [Header(" Attack ")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    [SerializeField] private float playerDetectionRadius;
    private float attackDelay;
    private float attackTimer;
    private bool isAttacking = false;
    private Vector2 lastAttackDirection;

    [Header(" Actions ")]
    public static Action<int, Vector2, bool> onDamageTaken;
    public static Action<Vector2> onPassedAway;

    [Header(" Debug ")]
    [SerializeField] private bool gizmos;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthText.text = health.ToString();

        movement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();

        player = FindFirstObjectByType<Player>();

        if (player == null)
        {
            Debug.LogWarning("No player found");
            Destroy(gameObject);
        }

        StartSpawnSequence();

        attackDelay = 1f / attackFrequency;
        
        // Устанавливаем расстояние остановки в зависимости от радиуса атаки
        movement.SetStopDistance(playerDetectionRadius);
    }

    private void StartSpawnSequence()
    {
        SetRenderersVisibility(false);

        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);
    }
    private void SpawnSequenceCompleted()
    {
        SetRenderersVisibility(true);
        hasSpawned = true;

        enemyCollider.enabled = true;

        movement.StorePlayer(player);
    }

    private void SetRenderersVisibility(bool visibility)
    {
        enemyRenderer.enabled = visibility;
        spawnIndicator.enabled = !visibility;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        
        // Обновляем направление атаки
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        if (directionToPlayer.magnitude > 0.1f)
        {
            lastAttackDirection = directionToPlayer;
            // Отзеркаливаем спрайт в зависимости от направления движения
            if (!isAttacking) // Не меняем отзеркаливание во время атаки
            {
                enemyRenderer.flipX = directionToPlayer.x > 0;
            }
        }
        
        // Если враг в радиусе атаки
        if (distanceToPlayer <= playerDetectionRadius)
        {
            if (attackTimer >= attackDelay)
            {
                Attack();
            }
            else
            {
                Wait();
            }
        }
        else
        {
            // Если враг вне радиуса атаки, сбрасываем таймер
            attackTimer = 0;
        }
    }

    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }

    private void Attack()
    {
        attackTimer = 0;
        TriggerAttackAnimation();
        player.TakeDamage(damage);
    }

    public void TakeDamage(int damage, bool isCriticalHit)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        healthText.text = health.ToString();

        onDamageTaken?.Invoke(damage, transform.position, isCriticalHit);

        if (health <= 0)
            PassAway();
    }

    private void PassAway()
    {
        onPassedAway?.Invoke(transform.position);

            Destroy(gameObject);
    }

    private void TriggerAttackAnimation()
    {
        if (!isAttacking)
        {
            isAttacking = true;

            // Запускаем анимацию атаки
            animator.SetTrigger("Attack");
            StartCoroutine(AttackSequence());
        }
    }

    private System.Collections.IEnumerator AttackSequence()
    {
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        if (!gizmos)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}

