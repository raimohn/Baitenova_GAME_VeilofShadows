using UnityEngine;

public class DrawGameZone : MonoBehaviour
{
    public Vector2 size = new Vector2(10, 5); // Размер зоны

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Цвет контура
        Gizmos.DrawWireCube(transform.position, new Vector3(size.x, size.y, 0)); // Контур зоны
    }
}
