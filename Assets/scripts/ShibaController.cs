using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShibaController : MonoBehaviour
{
    float frame = 0f;
    public Animator anim;

    private ShibaControls playerinput;
    private CharacterController controller;

    private Transform cameraMain;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool IsPunching = false;
    private bool IsKicking = false;
    private bool IsJumping = false;
    private float RefreshRate;
    private bool velocityGiven = false;

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
        
        Debug.Log("Refresh = " + RefreshRate);
        cameraMain = Camera.main.transform;
    }

    private bool IsGrounded()  {
        return true;
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
        float mag = Vector3.Magnitude(move);
        if (mag > 0.7)
            playerSpeed = 3.0f;
        else
            playerSpeed = 2.0f;

        controller.Move(move * Time.deltaTime * playerSpeed);
        anim.SetFloat("moveSpeed", mag);
        if (anim.GetFloat("moveSpeed") > 0.7 && mag < 0.7)
        {
            anim.SetFloat("moveSpeed", mag);
        }
        else if(anim.GetFloat("moveSpeed") < 0.7 && mag > 0.7)
        {
            anim.SetFloat("moveSpeed", mag);
        }
        if (move != Vector3.zero)
        {
            anim.SetBool("move", true);
            gameObject.transform.forward = move;
        }
        else
        {
            anim.SetBool("move", false);
        }
        
        if (playerinput.PlayerMain.Jump.triggered && groundedPlayer)
        {
            if(!IsJumping)
            IsJumping = true;
            //anim.SetTrigger("Jumping");
            anim.SetBool("jump",true);
        }
        else
        {
            anim.SetBool("jump", false);
        }
        if(IsJumping)
        {
            frame = frame + 1f;
        }
        RefreshRate = 1f / Time.deltaTime;
        if(frame >= RefreshRate/2 && velocityGiven == false)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            velocityGiven = true;
        }
        if(frame > (RefreshRate * 1.8f))
        {
            frame = 0;
            IsJumping = false;
            velocityGiven = false;
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
