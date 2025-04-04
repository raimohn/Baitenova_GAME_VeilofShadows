using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private MobileJoystick playerJoystick;
    [SerializeField] private Animator animator;
    private Rigidbody2D rig;

    [Header(" Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackCooldown = 0.5f;

    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.right; // Инициализируем вправо
    private float lastAttackTime = -1f;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rig.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        moveInput = playerJoystick.GetMoveVector().normalized;

        if (moveInput.sqrMagnitude > 0.01f)
        {
            rig.velocity = moveInput * moveSpeed * Time.fixedDeltaTime; // Добавили Time.fixedDeltaTime

            // Ограничиваем направление только по X (влево/вправо)
            if (moveInput.sqrMagnitude > 0.01f)
            {
                rig.velocity = playerJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
                lastMoveDirection = moveInput;
            }
        }
        else
        {
            rig.velocity = Vector2.zero;
        }

        // Управляем анимацией
        animator.SetFloat("MoveX", lastMoveDirection.x);
        animator.SetBool("isWalking", moveInput.sqrMagnitude > 0.01f);
    }



    public void Attack()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetFloat("AttackDirectionX", lastMoveDirection.x);
            animator.SetTrigger("Attack");
        }
    }
}
