using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] private Player player;

    private bool grounded;

    private void FixedUpdate()
    {
        grounded = isGrounded();
    }

    private void Update()
    {
        Jump();
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            float movementSpeed = player.playerStats.speed;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                movementSpeed = player.playerStats.sprintSpeed;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                movementSpeed = player.playerStats.crouchSpeed;
            }

            controller.Move(moveDir * movementSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            playerVelocity.y += Mathf.Sqrt(player.playerStats.jumpHeight * -3.0f * player.playerStats.gravity);
        }

        playerVelocity.y += player.playerStats.gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private bool isGrounded()
    {
        Debug.DrawRay(transform.position, -Vector3.up * ((controller.height * 0.5f) + 0.1f), Color.red);

        // bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // this would cast rays only against colliders in layer 8
        // but instead we want to collide against everything except layer 8. The ~ operator does this, it inverts bitmask
        layerMask = ~layerMask;

        RaycastHit hit;
        // ignore layer 8 (the player layer)
        //if (Physics.Raycast(transform.position, -Vector3.up, out hit, (controller.height * 0.5f) + 0.1f, layerMask))
        //{
        //    return true;
        //}

        if (Physics.SphereCast(transform.position, controller.radius, -Vector3.up, out hit, controller.bounds.extents.y + 0.1f - controller.bounds.extents.x, layerMask))
        {
            return true;
        }

        return false;
    }
}
