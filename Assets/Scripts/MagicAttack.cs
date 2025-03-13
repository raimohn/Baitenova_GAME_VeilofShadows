using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicAttack : MonoBehaviour
{
    public GameObject firePoint; // Точка, откуда выпускается магия
    public GameObject spellPrefab; // Префаб магического снаряда
    public MobileJoystick joystick; // Джойстик
    public Button attackButton;

    // Start is called before the first frame update
    void Start()
    {
        attackButton.onClick.AddListener(Fire);
    }

    void Fire()
    {
        if (joystick.GetMoveVector().magnitude > 0.1f) // Проверяем, есть ли направление атаки
        {
            GameObject spell = Instantiate(spellPrefab, firePoint.transform.position, Quaternion.identity);
            Spell spellScript = spell.GetComponent<Spell>(); // Получаем скрипт Spell
            spellScript.SetDirection(joystick.GetMoveVector().normalized); // Передаём направление атаки
        }
    }
}