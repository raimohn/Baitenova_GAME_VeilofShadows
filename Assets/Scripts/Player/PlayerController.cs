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
    private Vector2 lastMoveDirection;
    private float lastAttackTime = -1f;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rig.velocity = Vector3.zero;
        lastMoveDirection = Vector2.down;
    }


    private void FixedUpdate()
    {
        moveInput = playerJoystick.GetMoveVector().normalized;

        if (moveInput.sqrMagnitude > 0.01f)
        {
            rig.velocity = playerJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
            lastMoveDirection = moveInput;
        }
        else 
        {
            rig.velocity = Vector2.zero;
        }

        animator.SetFloat("MoveX", lastMoveDirection.x);
        animator.SetFloat("MoveY", lastMoveDirection.y);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);
        animator.SetBool("isWalking", moveInput.sqrMagnitude > 0.01f);
    }
    public void Attack()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetFloat("AttackDirectionX", lastMoveDirection.x);
            animator.SetFloat("AttackDirectionY", lastMoveDirection.y);
            animator.SetTrigger("Attack");
        }
    }

}
