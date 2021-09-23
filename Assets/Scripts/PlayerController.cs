using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController playerCtrl;
    public Animator animator;

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
        {
            animator.SetBool("isJumping", false);
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1);

        animator.SetFloat("Speed", moveDirection.magnitude);

        moveDirection *= moveSpeed;

        if(Input.GetButtonDown("Jump"))
        {
            if (playerCtrl.isGrounded)
            {
                if(!animator.GetBool("isJumping"))
                    animator.SetBool("isJumping", true);
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
