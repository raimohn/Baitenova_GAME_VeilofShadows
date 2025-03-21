using UnityEngine;

public class Spell : MonoBehaviour
{
    private Vector3 direction; // ��������� direction ��� ���� ������
    public float speed = 10f; // Скорость движения заклинания
    public float lifetime = 3f;
    private bool isStopped = false;

    void Start()
    {
        // Уничтожаем заклинание через lifetime секунд
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        if (!isStopped)
        {
            // Движение заклинания в заданном направлении
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Уничтожаем заклинание при столкновении с любым коллайдером
        Destroy(gameObject);
    }
}