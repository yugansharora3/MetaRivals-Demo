using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    private Animator anim;

    private ShibaControls playerinput;
    private CharacterController controller;

    private Transform cameraMain;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool IsPunching = false;
    private bool IsKicking = false;
    bool CoinCollected = false;

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
        anim = GetComponent<Animator>();
        playerinput = new ShibaControls();
        controller = GetComponent<CharacterController>();
        controller.detectCollisions = true;
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
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = playerinput.PlayerMain.Move.ReadValue<Vector2>();
        Vector3 move = cameraMain.forward * movementInput.y + cameraMain.right * movementInput.x;
        move.y = 0f;
        float mag = Vector3.Magnitude(move);
        float moveSpeed;
        if (mag > 0.7)
            moveSpeed = playerSpeed * 1.5f;
        else
            moveSpeed = playerSpeed;

        controller.Move(moveSpeed * Time.deltaTime * move);
        anim.SetFloat("moveSpeed", mag);

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
            anim.SetBool("jump",true);
            if (!JumpingSound.isPlaying)
            {
                JumpingSound.Play();
            }
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
            PunchingSound.Play();
        }
        else
        {
            IsPunching = false;
        }

        if (playerinput.PlayerMain.Kick.triggered && !IsKicking)
        {
            IsKicking = true;
            anim.SetTrigger("Kicking");
            KickingSound.Play();
        }
        else
        {
            IsKicking = false;
        }
        
        if(CoinCollected)
            CoinCollected = false;
    }

    void AddVelocity()
    {
        playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Coin" && !CoinCollected)
        {
            ScoreManager scoreManager = GameObject.Find("Manager").GetComponent<ScoreManager>();
            if(scoreManager.score < scoreManager.MaxCoins)
            {
                CoinCollected = true;
                //Debug.Log("collision with coin");
                hit.gameObject.SetActive(false);
                scoreManager.IncreaseScore();
                scoreManager.UpdateScore();
            }
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
