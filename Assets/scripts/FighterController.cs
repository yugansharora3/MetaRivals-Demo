using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    private Animator anim;

    private ShibaControls playerinput;
    private PlayerCommon common;
    private Vector3 playerVelocity;
    private Vector2 forward;

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
        common = GetComponent<PlayerCommon>();
    }

    void Update()
    {
        Vector2 movementInput = playerinput.PlayerMain.Move.ReadValue<Vector2>();

        if(common.CanChangeHeight)
        {
            anim.SetFloat("height",movementInput.y);
        }
        anim.SetFloat("moveSpeed", movementInput.x);
        if(common.CanChangeAttackDefend)
        {
            anim.SetFloat("AttackDefend", movementInput.x);
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
        
        if (movementInput.x == 0)
        {
            if(!common.IsIdle)
            {
                anim.SetBool("Moving", false);
                anim.SetTrigger("IdleTrig");
                common.IsIdle = true;
                common.IsMoving = false;
            }
        }
        else
        {
            anim.SetBool("Moving", true);
            common.IsMoving = true;
            common.IsIdle = false;
        }
        
        if (playerinput.PlayerMain.Jump.triggered && !common.IsJumping)
        {
            anim.SetBool("jump",true);
            anim.SetTrigger("Jumping");
            common.IsJumping = true;
            if (!JumpingSound.isPlaying)
            {
                JumpingSound.Play();
            }
        }
        else
        {
            anim.SetBool("jump", false);
        }
        if (playerinput.PlayerMain.Punch.triggered && !common.IsPunching)
        {
            common.IsPunching = true;
            anim.SetTrigger("Punching");
            PunchingSound.Play();
            common.CanChangeHeight = false;
            common.CanChangeAttackDefend = false;
        }

        if (playerinput.PlayerMain.Kick.triggered && !common.IsKicking)
        {
            common.IsKicking = true;
            anim.SetTrigger("Kicking");
            KickingSound.Play();
            common.CanChangeHeight = false;
            common.CanChangeAttackDefend = false;
        }
        
    }

    void AddVelocity()
    {
        playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

    void PlayJumpSound()
    {
        if (!JumpingSound.isPlaying)
        {
            JumpingSound.Play();
        }
    }

}
