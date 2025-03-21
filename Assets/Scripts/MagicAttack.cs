using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MagicAttack : MonoBehaviour
{
    public MobileJoystick joystick;
    public Button attackButton;
    public Animator animator;
    private bool isAttacking = false;
    private Vector2 lastDirection = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        attackButton.onClick.AddListener(Attack);
    }

    void Update()
    {
        // Проверяем нажатие пробела для атаки
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        // Обновляем направление движения
        Vector2 currentDirection = joystick.GetMoveVector();
        if (currentDirection.magnitude > 0.1f)
        {
            lastDirection = currentDirection;
        }

        // Обновляем параметры анимации
        UpdateAnimationParameters();
    }

    void UpdateAnimationParameters()
    {
        // Устанавливаем параметры направления для blend trees
        animator.SetFloat("MoveX", lastDirection.x);
        animator.SetFloat("MoveY", lastDirection.y);
        animator.SetFloat("Speed", joystick.GetMoveVector().magnitude);
        
        // Инвертируем X для правильного направления атаки
        animator.SetFloat("AttackDirectionX", -lastDirection.x);
        animator.SetFloat("AttackDirectionY", lastDirection.y);
    }

    void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            StartCoroutine(AttackSequence());
        }
    }

    System.Collections.IEnumerator AttackSequence()
    {
        // Ждем окончания анимации атаки
        yield return new WaitForSeconds(0.5f);
        
        // Возвращаемся к предыдущему состоянию
        isAttacking = false;
    }
}