using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Swipe : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    public float lookSpeed = 2.0f, XSenstivity = 1.0f, YSenstivity = 0.01f;
    public bool OnPc = false;
    private bool isDragging;

    public Vector2 startTouch, swipeDelta;
    public bool Pressed = false;
    public int PointerId;

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

    // Update is called once per frame
    void Update()
    {
        if(OnPc)
        {
            XSenstivity = 10f;
            YSenstivity = 0.1f;
        }

        //#region Standalone Inputs
        ////if (Input.GetMouseButtonDown(0))
        ////{
        ////    if (CheckIfPointIsOnLook(Input.mousePosition))
        ////    {
        ////        //tap = true;
        ////        isDragging = true;
        ////        startTouch = Input.mousePosition;
        ////    }

        ////}
        ////else if (Input.GetMouseButtonUp(0))
        ////{
        ////    isDragging = false;
        ////    Reset();
        ////}
        //#endregion

        //#region Mobile Inputs
        ////int lastTouchIndex = Input.touches.Length - 1;
        ////int lastTouchIndex = PointerId;
        ////if (Input.touches.Length > 0 && PointerId >= 0 && PointerId < Input.touches.Length)
        ////{
        ////    if (Input.touches[lastTouchIndex].phase == TouchPhase.Began)
        ////    {
        ////        if (CheckIfPointIsOnLook(Input.touches[lastTouchIndex].position))
        ////        {
        ////            //tap = true;
        ////            isDragging = true;
        ////            startTouch = Input.touches[lastTouchIndex].position;

        ////        }

        ////    }
        ////    else if (Input.touches[lastTouchIndex].phase == TouchPhase.Ended || Input.touches[lastTouchIndex].phase == TouchPhase.Canceled)
        ////    {
        ////        isDragging = false;
        ////        Reset();
        ////    }
        ////}
        //#endregion

        //#region Calculate Swipe Delta
        ////swipeDelta = Vector2.zero;
        ////if (Pressed)
        ////{
        ////    if (Input.touches.Length > 0 && PointerId >= 0 && PointerId < Input.touches.Length)
        ////    {
        ////        if (CheckIfPointIsOnLook(Input.touches[lastTouchIndex].position))
        ////        {
        ////            swipeDelta = Input.touches[lastTouchIndex].position - startTouch;
        ////        }
        ////    }
        ////    else if (Input.GetMouseButton(0))
        ////    {
        ////        if (CheckIfPointIsOnLook(Input.mousePosition))
        ////        {
        ////            swipeDelta = (Vector2)Input.mousePosition - startTouch;
        ////            startTouch = (Vector2)Input.mousePosition;
        ////        }
        ////    }

        ////}
        //#endregion

        if(Pressed)
        {
            cinemachine.m_XAxis.Value += swipeDelta.x * XSenstivity * lookSpeed * Time.deltaTime;
            cinemachine.m_YAxis.Value += -swipeDelta.y * YSenstivity * lookSpeed * Time.deltaTime;
        }
        
        swipeDelta = Vector2.zero;
        //#region More Mobile Input
        ////if (Input.touches.Length > 0 && PointerId >= 0 && PointerId < Input.touches.Length)
        ////{
        ////    if (Input.touches[lastTouchIndex].phase == TouchPhase.Moved)
        ////    {
        ////        if (CheckIfPointIsOnLook(Input.touches[lastTouchIndex].position))
        ////        {
        ////            //tap = true;
        ////            isDragging = true;
        ////            startTouch = Input.touches[lastTouchIndex].position;
        ////        }
        ////    }
        ////}
        //#endregion
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    private bool CheckIfPointIsOnLookOnly(Vector2 position)
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = position;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results.Count == 1)
        {
            if (results[0].gameObject.name.Equals("Look"))
            {
                //Debug.Log("Clicked on Look");
                return true;
            }
        }
        return false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        if (CheckIfPointIsOnLookOnly(eventData.position))
        {
            Pressed = true;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        Reset();
    }
    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    Pressed = false;
    //    Reset();
    //}

    public void OnDrag(PointerEventData eventData)
    {
        swipeDelta = eventData.position - startTouch;
        startTouch = eventData.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CheckIfPointIsOnLookOnly(eventData.position))
        {
            startTouch = eventData.position;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Reset();
    }

}
