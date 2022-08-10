using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = 0.3f;

    [SerializeField]
    private float lookSpeed = 1f;

    private float look = 0f;

    private float maximumTime = 1f;

    private InputManager inputManager;
    private Vector2 startPosition, endPosition;
    private float startTime, endTime;

    private CinemachineFreeLook cinemachine;
    private ShibaControls playerInput;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        playerInput = new ShibaControls();
        cinemachine = GetComponent<CinemachineFreeLook>();
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
        playerInput.Enable();
    }
    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
        playerInput.Disable();
    }

    private void SwipeStart(Vector2 position,float time)
    {
        startPosition = position;
        startTime = time;
    }
    private void SwipeEnd(Vector2 position,float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        look = 0f;
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
        {
            Vector2 delta = endPosition - startPosition;
            look += delta.x * 50 * lookSpeed ;
        }
    }

    private void Update()
    {
        //if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
        //{
        //    Vector2 delta = endPosition - startPosition;
        //    cinemachine.m_XAxis.Value += delta.x * 50 * lookSpeed * Time.deltaTime;
        //    //Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
        //}
        if(look > 0)
        {
            cinemachine.m_XAxis.Value += 0.1f;
            look -= 0.1f;
        }
    }
}
