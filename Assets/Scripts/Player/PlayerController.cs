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

    private Vector2 moveInput;


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
        rig.velocity = playerJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;


        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);
    }
}
