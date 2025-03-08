using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private MobileJoystick playerJoystick;
    private Rigidbody2D rig;

    [Header(" Settings")]
    [SerializeField] private float moveSpeed;



    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = Vector3.zero;
    }


    private void FixedUpdate()
    {
        rig.velocity = playerJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
    }
}
