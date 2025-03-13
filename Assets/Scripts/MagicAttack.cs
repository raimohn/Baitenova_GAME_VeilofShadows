using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicAttack : MonoBehaviour
{
    public GameObject firePoint; // �����, ������ ����������� �����
    public GameObject spellPrefab; // ������ ����������� �������
    public MobileJoystick joystick; // ��������
    public Button attackButton;

    // Start is called before the first frame update
    void Start()
    {
        attackButton.onClick.AddListener(Fire);
    }

    void Fire()
    {
        if (joystick.GetMoveVector().magnitude > 0.1f) // ���������, ���� �� ����������� �����
        {
            GameObject spell = Instantiate(spellPrefab, firePoint.transform.position, Quaternion.identity);
            Spell spellScript = spell.GetComponent<Spell>(); // �������� ������ Spell
            spellScript.SetDirection(joystick.GetMoveVector().normalized); // ������� ����������� �����
        }
    }
}