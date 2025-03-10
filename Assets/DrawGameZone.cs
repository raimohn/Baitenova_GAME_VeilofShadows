using UnityEngine;

public class DrawGameZone : MonoBehaviour
{
    public Vector2 size = new Vector2(10, 5); // ������ ����

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // ���� �������
        Gizmos.DrawWireCube(transform.position, new Vector3(size.x, size.y, 0)); // ������ ����
    }
}
