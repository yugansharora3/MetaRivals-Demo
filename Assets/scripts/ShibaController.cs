using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShibaController : MonoBehaviour
{
    public Animator anim;

    private ShibaControls playerinput;
    private CharacterController controller;

    private Transform cameraMain;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool IsPunching = false;
    private bool IsKicking = false;
    private float distToGround;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private void Awake()
    {
        playerinput = new ShibaControls();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        playerinput.Enable();
    }

    private void OnDisable()
    {
        playerinput.Disable();
    }

    private void Start()
    {
        cameraMain = Camera.main.transform;
        //distToGround = this.GetComponent<Collider>.bounds.extents.y;
    }

    private bool IsGrounded()  {
        return true;
        //return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.1));
    }
void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = playerinput.PlayerMain.Move.ReadValue<Vector2>();
        Vector3 move = cameraMain.forward * movementInput.y + cameraMain.right * movementInput.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("punch"))
        {
            // Avoid any reload.
        }

        if (move != Vector3.zero && !anim.GetCurrentAnimatorStateInfo(0).IsName("kick") && !anim.GetCurrentAnimatorStateInfo(0).IsName("punch"))
        {
            anim.SetBool("move", true);
            gameObject.transform.forward = move;
        }
        else
        {
            anim.SetBool("move", false);
        }

        // Changes the height position of the player..
        if (playerinput.PlayerMain.Jump.triggered )
        {
            //playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(playerinput.PlayerMain.Punch.triggered && !IsPunching)
        {
            IsPunching = true;
            anim.SetTrigger("Punching");
        }
        else
        {
            IsPunching = false;
        }

        if (playerinput.PlayerMain.Kick.triggered && !IsKicking)
        {
            IsKicking = true;
            anim.SetTrigger("Kicking");
        }
        else
        {
            IsKicking = false;
        }
    }
}
