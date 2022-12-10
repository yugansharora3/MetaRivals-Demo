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
    [SerializeField]
    private AudioSource RunningSound;
    [SerializeField]
    private AudioSource WalkingSound;

    public GameObject TextObj;
    public GameObject CoinGenerator;
    public int MaxCoins = 20;

    //public FixedJoystick LeftJoystick;
    private void Awake()
    {
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
        TextObj.transform.rotation = Camera.main.transform.rotation;
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
        //if (anim.GetFloat("moveSpeed") > 0.7 && mag < 0.7)
        //{
        //    anim.SetFloat("moveSpeed", mag);
        //}
        //else if(anim.GetFloat("moveSpeed") < 0.7 && mag > 0.7)
        //{
        //    anim.SetFloat("moveSpeed", mag);
        //}

        if (move != Vector3.zero)
        {
            anim.SetBool("move", true);
            gameObject.transform.forward = move;
            if(!JumpingSound.isPlaying && !KickingSound.isPlaying && !PunchingSound.isPlaying)
            {
                if (mag < 0.7)
                {
                    PlayWalkingSound();
                }
                else
                {
                    PlayRunningSound();
                }
            }
        }
        else
        {
            anim.SetBool("move", false);
            if (WalkingSound.isPlaying)
            {
                WalkingSound.Stop();
            }
            if (RunningSound.isPlaying)
            {
                RunningSound.Stop();
            }
        }
        
        if (playerinput.PlayerMain.Jump.triggered && groundedPlayer)
        {
            anim.SetBool("jump",true);
            if (WalkingSound.isPlaying)
            {
                WalkingSound.Stop();
            }
            if (RunningSound.isPlaying)
            {
                RunningSound.Stop();
            }
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
            if (WalkingSound.isPlaying)
            {
                WalkingSound.Stop();
            }
            if (RunningSound.isPlaying)
            {
                RunningSound.Stop();
            }
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
            if(WalkingSound.isPlaying)
            {
                WalkingSound.Stop();
            }
            if (RunningSound.isPlaying)
            {
                RunningSound.Stop();
            }
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
            GenerateCoin generator =  CoinGenerator.GetComponent<GenerateCoin>();
            if(generator.score < MaxCoins)
            {
                CoinCollected = true;
                Debug.Log("collision with coin");
                hit.gameObject.SetActive(false);
                generator.IncreaseScore();
                generator.UpdateScore();
            }
        }
    }

    void PlayWalkingSound()
    {
        if (!WalkingSound.isPlaying)
        {
            WalkingSound.Play();
        }
        if (RunningSound.isPlaying)
        {
            RunningSound.Stop();
        }
    }
    void PlayRunningSound()
    {
        if (WalkingSound.isPlaying)
        {
            WalkingSound.Stop();
        }
        if (!RunningSound.isPlaying)
        {
            RunningSound.Play();
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
