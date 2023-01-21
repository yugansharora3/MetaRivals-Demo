using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class FighterController : MonoBehaviour
{
    private Animator anim;

    private ShibaControls playerinput;
    //private CharacterController controller;

    private Transform cameraMain;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool IsPunching = false;
    private bool IsKicking = false;
    private bool IsJumping = false;
    private bool IsIdle = false;
    private bool IsMoving = false;
    private Vector2 forward;
    bool CanChangeHeight = true;

    [SerializeField]
    private float playerSpeed = 25.0f;
    [SerializeField]
    private float jumpHeight = 3.0f;
    [SerializeField]
    private float gravityValue = -30f;
    [SerializeField]
    private AudioSource JumpingSound;
    [SerializeField]
    private AudioSource KickingSound;
    [SerializeField]
    private AudioSource PunchingSound;
    private void Awake()
    {
        forward = transform.forward;
        anim = GetComponent<Animator>();
        playerinput = new ShibaControls();
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
    }

    void Update()
    {
        //groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = playerinput.PlayerMain.Move.ReadValue<Vector2>();

        if(CanChangeHeight)
        {
            anim.SetFloat("height",movementInput.y);
        }

        Vector3 move = forward * movementInput.x;
        move.y = 0f;
        float mag = Vector3.Magnitude(move);
        float moveSpeed;
        if (mag > 0.7)
            moveSpeed = playerSpeed * 1.5f;
        else
            moveSpeed = playerSpeed;

        transform.position += moveSpeed * Time.deltaTime * move * 0.1f;
        //controller.Move(moveSpeed * Time.deltaTime * move);
        anim.SetFloat("moveSpeed", movementInput.x);
        if (movementInput.x == 0 && !IsMoving)
        {
            if(!IsIdle)
            {
                anim.SetTrigger("IdleTrig");
                IsIdle = true;
                IsMoving = false;
            }
        }
        else
        {
            if(!IsMoving)
            {
                anim.SetTrigger("Move");
                IsMoving = true;
            }
            IsIdle = false;
        }

        if (move != Vector3.zero)
        {
            //anim.SetBool("move", true);
            //gameObject.transform.forward = move;
        }
        else
        {
            //anim.SetBool("move", false);
        }
        
        if (playerinput.PlayerMain.Jump.triggered && !IsJumping)
        {
            anim.SetBool("jump",true);
            anim.SetTrigger("Jumping");
            IsJumping = true;
            if (!JumpingSound.isPlaying)
            {
                JumpingSound.Play();
            }
        }
        else
        {
            anim.SetBool("jump", false);
        }
        
        //playerVelocity.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);
        //transform.position += playerVelocity * Time.deltaTime;

        if (playerinput.PlayerMain.Punch.triggered && !IsPunching)
        {
            IsPunching = true;
            anim.SetTrigger("Punching");
            PunchingSound.Play();
            CanChangeHeight = false;
        }

        if (playerinput.PlayerMain.Kick.triggered && !IsKicking)
        {
            IsKicking = true;
            anim.SetTrigger("Kicking");
            KickingSound.Play();
            CanChangeHeight = false;
        }
        
    }

    void Set()
    {
        IsJumping = false;
        IsKicking = false;
        IsPunching = false;
        
        IsMoving = false;
        CanChangeHeight = true;
    }

    void SetJump()
    {
        IsKicking = false;
        IsPunching = false;
    }
    void SetPunch()
    {
        IsKicking = false;
        IsJumping = false;
    }
    void SetKick()
    {
        IsPunching = false;
        IsJumping = false;
    }
    void UnSetJump()
    {
        IsJumping = false;
        anim.SetTrigger("IdleTrig");
        CanChangeHeight = true;
    }
    void UnSetPunch()
    {
        IsPunching = false;
        anim.SetTrigger("IdleTrig");
        CanChangeHeight = true;
    }
    void UnSetKick()
    {
        IsKicking = false;
        anim.SetTrigger("IdleTrig");
        CanChangeHeight = true;
    }
    void UnSetMoving()
    {
        IsMoving = false;
    }

    void AfterGettingUp()
    {
        anim.SetTrigger("IdleTrig");
    }

    void AddVelocity()
    {
        playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Coin" )
        {
            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hittable")
        {
            Debug.Log("Hit");
        }
    }

    void PlayJumpSound()
    {
        if (!JumpingSound.isPlaying)
        {
            JumpingSound.Play();
        }
    }

}
