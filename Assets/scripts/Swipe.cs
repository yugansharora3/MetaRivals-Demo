using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Swipe : MonoBehaviour
{
    [SerializeField]
    private float lookSpeed = 2f;
    static float t = 0.0f;
    public bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDragging;
    public Vector2 startTouch, swipeDelta,storedDelta;
    private GameObject[] UIElements;

    private CinemachineFreeLook cinemachine;
    void Start()
    {

    }
    private void Awake()
    {
        cinemachine = GetComponent<CinemachineFreeLook>();
        storedDelta = Vector2.zero;
    }
    // Update is called once per frame
    void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        int lastTouchIndex = Input.touches.Length - 1;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            if (check())
            {
                tap = true;
                isDragging = true;
                startTouch = Input.mousePosition;
            }
                
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
        }
        #endregion

        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[lastTouchIndex].phase == TouchPhase.Began)
            {
                if(!CheckIfPointIsOnUI(Input.touches[lastTouchIndex].position))
                {
                    if(check())
                    {
                        tap = true;
                        isDragging = true;
                        startTouch = Input.touches[lastTouchIndex].position;
                    }
                    
                }
                
            }
            else if (Input.touches[lastTouchIndex].phase == TouchPhase.Ended || Input.touches[lastTouchIndex].phase == TouchPhase.Canceled )
            {
                isDragging = false;
                Reset();
            }
        }
        #endregion
        
        swipeDelta = Vector2.zero;
        if(isDragging)
        {

            if (Input.touches.Length > 0)
            {
                if (!CheckIfPointIsOnUI(Input.touches[lastTouchIndex].position))
                {
                    if(check())
                    {
                        swipeDelta = Input.touches[lastTouchIndex].position - startTouch;

                    }
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (check())
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }

        }
                
        t += (5f * Time.deltaTime);
        float xval = Mathf.Lerp(0f, swipeDelta.x * 1 * lookSpeed * Time.deltaTime, t);
        cinemachine.m_XAxis.Value += swipeDelta.x * 1 * lookSpeed * Time.deltaTime;
        float yval = Mathf.Lerp(0f, -swipeDelta.y * 0.01f * lookSpeed * Time.deltaTime, t);
        cinemachine.m_YAxis.Value += -swipeDelta.y * 0.01f * lookSpeed * Time.deltaTime;
        
        

        if (Input.touches.Length > 0)
        {
            if (Input.touches[lastTouchIndex].phase == TouchPhase.Moved)
            {
                if (!CheckIfPointIsOnUI(Input.touches[lastTouchIndex].position))
                {
                    
                        tap = true;
                        isDragging = true;
                        startTouch = Input.touches[lastTouchIndex].position;
                    
                }
            }
        }

        if (t > 1.0f)
        {
            t = 0.0f;
            storedDelta = Vector2.zero;
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    private bool CheckIfPointIsOnUI(Vector2 position)
    {
        if (position.x > 0 && position.x < 500f && position.y > 0f && position.y < 500f)
        {
            return true;
        }       
        return false;
    }

    
    private bool check()
    {
        if (startTouch.x > 35 && startTouch.x < 135 && startTouch.y > 35 && startTouch.y < 135)
        {
            return false;
        }
        return true;
    }
}
