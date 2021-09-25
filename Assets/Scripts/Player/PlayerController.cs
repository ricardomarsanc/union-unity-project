using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    public ParticleSystem dust;

    public CharacterController playerCtrl;
    public Animator animator;

    public InventoryObject playerInventory;

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

    private GameObject activeItem;
    private GameObject grabItemText;

    private void Start()
    {
        
        gameManager = FindObjectOfType<GameManager>();

        grabItemText = gameManager.UI_GrabItemText;
        playerInventory = gameManager.PlayerInventory;

        dust = GetComponentInChildren<ParticleSystem>();

        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if(playerCtrl == null)
        {
            playerCtrl = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && activeItem)
        {
            GrabItem(activeItem);
        }

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

                CreateDust();
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

    void GrabItem(GameObject activeItem)
    {
        if(activeItem != null)
        {
            FindObjectOfType<AudioManager>().Play("GrabItem");
            grabItemText.SetActive(false);
            var item = activeItem.GetComponent<Item>();
            // Add the item to the generic Inventory which will not be destroyed with the player and then re-asign the inventory to the player
            gameManager.PlayerInventory.AddItem(item.itemObject);
            playerInventory = gameManager.PlayerInventory;
            Destroy(activeItem);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            // @TODO: Play dead animation
            gameManager.GetComponent<PlayerLifeSystem>().Die();
        }
        else
        {
            // INVENTORY SYSTEM
            var item = other.GetComponent<Item>();
            if (item)
            {
                // Display Text
                grabItemText.SetActive(true);
                activeItem = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            // Hide Text
            grabItemText.SetActive(false);
            activeItem = null;
        }
    }

    void CreateDust()
    {
        dust.Play();
    }
}
