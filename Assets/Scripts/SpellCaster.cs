using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public GameObject spellPrefab;
    public MobileJoystick joystick;

    void Update()
    {
        if (joystick != null && Input.GetMouseButtonUp(0)) // или при нажатии на кнопку стрельбы
        {
            CastSpell();
        }
    }

    void CastSpell()
    {
        if (spellPrefab != null && joystick != null)
        {
            Vector3 moveDirection = joystick.GetMoveVector();

            if (moveDirection != Vector3.zero)
            {
                GameObject spellObject = Instantiate(spellPrefab, transform.position, Quaternion.identity);
                Spell spell = spellObject.GetComponent<Spell>();

                if (spell != null)
                {
                    // Преобразуем вектор из экранных координат в мировые
                    Vector3 worldDirection = Camera.main.ScreenToWorldPoint(new Vector3(moveDirection.x + joystick.transform.position.x, moveDirection.y + joystick.transform.position.y, 10f)) - transform.position;
                    worldDirection.z = 0;
                    worldDirection.Normalize();

                    spell.SetDirection(worldDirection);
                }
            }
        }
    }
}