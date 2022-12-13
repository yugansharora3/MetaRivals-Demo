using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Swipe : MonoBehaviour
{
    [SerializeField]
    public float lookSpeed = 2.0f, XSenstivity = 1.0f, YSenstivity = 0.01f;
    public bool OnPc = false;
    static float t = 0.0f;
    //private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDragging;

    private Vector2 startTouch, swipeDelta,storedDelta;
    private bool Pressed = false;
    private int PointerId;
    private Vector2 PointerOld;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public Canvas canvas;

    public CinemachineFreeLook cinemachine;
    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }
    private void Awake()
    {
        //cinemachine = GetComponent<CinemachineFreeLook>();
        storedDelta = Vector2.zero;
    }
    // Update is called once per frame
    void Update()
    {
        if(OnPc)
        {
            XSenstivity = 10f;
            YSenstivity = 0.1f;
        }
        //tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        //int lastTouchIndex = Input.touches.Length - 1;
        int lastTouchIndex = PointerId;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckIfPointIsOnLook(Input.mousePosition))
            {
                //tap = true;
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
                if (CheckIfPointIsOnLook(Input.touches[lastTouchIndex].position))
                {
                    //tap = true;
                    isDragging = true;
                    startTouch = Input.touches[lastTouchIndex].position;

                }

            }
            else if (Input.touches[lastTouchIndex].phase == TouchPhase.Ended || Input.touches[lastTouchIndex].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }
        #endregion

        swipeDelta = Vector2.zero;
        if (isDragging)
        {

            if (Input.touches.Length > 0)
            {
                if (CheckIfPointIsOnLook(Input.touches[lastTouchIndex].position))
                {
                    swipeDelta = Input.touches[lastTouchIndex].position - startTouch;
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (CheckIfPointIsOnLook(Input.touches[lastTouchIndex].position))
                {
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
                }
            }

        }

        cinemachine.m_XAxis.Value += swipeDelta.x * XSenstivity * lookSpeed * Time.deltaTime;
        cinemachine.m_YAxis.Value += -swipeDelta.y * YSenstivity * lookSpeed * Time.deltaTime;

        if (Input.touches.Length > 0)
        {
            if (Input.touches[lastTouchIndex].phase == TouchPhase.Moved)
            {
                if (CheckIfPointIsOnLook(Input.touches[lastTouchIndex].position))
                {
                    //tap = true;
                    isDragging = true;
                    startTouch = Input.touches[lastTouchIndex].position;
                }
            }
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    private bool CheckIfPointIsOnLook(Vector2 position)
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = position;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results.Count > 0)
        {
            foreach(RaycastResult raycast in results)
            {
                if (raycast.gameObject.name.Equals("Look"))
                {
                    Debug.Log("Clicked on Look");
                    return true;
                }
            }
        }
        return false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Pressed = false;
    }
}
