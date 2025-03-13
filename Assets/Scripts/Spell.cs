using UnityEngine;

public class Spell : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 direction;

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle); // Поворачиваем снаряд
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime; // Двигаем снаряд
    }
}

