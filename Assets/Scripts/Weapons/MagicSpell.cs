using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MagicSpell : Weapon
{
    [Header(" Elements ")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet bulletPrefab;

    public MobileJoystick joystick;
    public Button attackButton;

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
            AutoAim();
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

    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();

        Vector2 targetUpVector = Vector3.up;

        if (closestEnemy != null)
        {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            transform.up = targetUpVector;

            ManageShooting();
            return;
        }
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
    }
    private void ManageShooting()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDelay)
        {
            attackTimer = 0;
            Attack();
        } 
    }

    void UpdateAnimationParameters()
    {
        // Устанавливаем параметры направления для blend trees
        animator.SetFloat("MoveX", lastDirection.x);
        animator.SetFloat("Speed", joystick.GetMoveVector().magnitude);
        
        // Инвертируем X для правильного направления атаки
        animator.SetFloat("AttackDirectionX", -lastDirection.x);
    }

    Vector2 GetAttackDirection()
    {
        Enemy closestEnemy = GetClosestEnemy();
        if (closestEnemy != null)
        {
            // Направление в сторону врага
            return (closestEnemy.transform.position - firePoint.position).normalized;
        }
        else
        {
            // Если врагов нет, используем последнее направление движения
            return lastDirection.normalized;
        }
    }


    void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            StartCoroutine(AttackSequence());

            Bullet bulletInstance = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 direction = GetAttackDirection(); // Получаем правильное направление
                rb.velocity = direction * 10f; // Задаём скорость
            }

            int damage = GetDamage(out bool isCriticalHit);

            bulletInstance.Shoot(damage, GetAttackDirection(), isCriticalHit);
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