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

        if(Pressed)
        {
            cinemachine.m_XAxis.Value += swipeDelta.x * XSenstivity * lookSpeed * Time.deltaTime;
            cinemachine.m_YAxis.Value += -swipeDelta.y * YSenstivity * lookSpeed * Time.deltaTime;
        }
        
        swipeDelta = Vector2.zero;
        
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        Pressed = false;
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
        Reset();
    }
    // Don't forget to add inheritance on class
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
