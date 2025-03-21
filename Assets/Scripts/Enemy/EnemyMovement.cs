using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header(" Elements ")]
    private Player player;

    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    private float stopDistance = 0.5f; // Минимальное расстояние до игрока

    private Vector2 currentDirection;

    // Update is called once per frame
    void Update()
    {
        if(player != null)
            FollowPlayer();
    }

    public void StorePlayer(Player player)
    {
        this.player = player;
    }

    public void SetStopDistance(float attackRadius)
    {
        // Устанавливаем расстояние остановки как 70% от радиуса атаки
        stopDistance = attackRadius * 0.7f;
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Если враг находится дальше минимального расстояния, двигаемся к игроку
        if (distanceToPlayer > stopDistance)
        {
            currentDirection = direction;
            Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;
            transform.position = targetPosition;
        }
        else
        {
            // Если враг слишком близко, останавливаемся
            currentDirection = Vector2.zero;
        }
    }

    public Vector2 GetCurrentDirection()
    {
        return currentDirection;
    }
}
