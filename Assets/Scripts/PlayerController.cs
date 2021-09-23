using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController playerCtrl;

    public Vector3 moveDirection;
    public Vector3 movePlayer;

    [SerializeField]private Vector3 velocity;

    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float jumpHeight = 11f;
    [SerializeField] float jumpHeightDouble = 4f;

    [SerializeField] bool doubleJumpCheck = false;

    public Camera mainCamera;
    public Vector3 camForward;
    public Vector3 camRight;

    [SerializeField] float gravity = -20f;
    [SerializeField] float gravityMultiplier = 2.5f;

    private void Start()
    {
        if(playerCtrl == null)
        {
            playerCtrl = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCtrl.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1);

        moveDirection *= moveSpeed;

        /*if (moveDirection.magnitude > 0.1f) {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 finalMoveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }*/

        if(Input.GetButtonDown("Jump"))
        {
            if (playerCtrl.isGrounded)
            {
                doubleJumpCheck = true;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else
            {
                if (doubleJumpCheck)
                {
                    velocity.y = Mathf.Sqrt(jumpHeightDouble * -2f * gravity);
                    doubleJumpCheck = false;
                }
            }
        }

        camDirection();

        movePlayer = moveDirection.x * camRight + moveDirection.z * camForward;
        playerCtrl.transform.LookAt(playerCtrl.transform.position + movePlayer);

        velocity.y += gravity * gravityMultiplier * Time.deltaTime;
        movePlayer.y = velocity.y;

        playerCtrl.Move(movePlayer * Time.deltaTime);

    }

    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

}
