using UnityEngine;

public class MagicSphere : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Уничтожаем сферу при столкновении с любым коллайдером
        Destroy(gameObject);
    }
} 