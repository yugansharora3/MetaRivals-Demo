using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float lookSpeed = 2.0f, XSenstivity = 1.0f,YSenstivity = 0.01f;
    public bool OnPc = false;
    [HideInInspector]
    public Vector2 TouchDist;
    [HideInInspector]
    public Vector2 PointerOld;
    [HideInInspector]
    protected int PointerId;
    [HideInInspector]
    public bool Pressed;
    public CinemachineFreeLook Cinemachine;


    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    // Use this for initialization
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }
    private bool CheckIfPointIsOnUI(Vector2 position)
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = position;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if(results.Count == 1)
        {
            if (results[0].gameObject.name == "Look")
            {
                return false;
            }
            else
            { return true; }
        }
        else
        {
            if (results.Count > 0)
                return true;
            else
                return false;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OnPc)
        {
            XSenstivity = 10;
            YSenstivity = 0.1f;
        }
        if (Pressed)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                if(!CheckIfPointIsOnUI(Input.touches[PointerId].position))
                {
                    TouchDist = Input.touches[PointerId].position - PointerOld;
                    PointerOld = Input.touches[PointerId].position;
                }
            }
            else
            {
                if(!CheckIfPointIsOnUI(Input.mousePosition))
                {
                    Vector2 temp = TouchDist;
                    TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                    
                    if(TouchDist.magnitude > 100)
                    {
                        Debug.Log(TouchDist.magnitude);
                        TouchDist = temp;
                    }
                    else
                        PointerOld = Input.mousePosition;
                }
            }
        }
        else
        {
            TouchDist = new Vector2();
        }
        Cinemachine.m_XAxis.Value += TouchDist.x * Time.deltaTime * lookSpeed * XSenstivity;
        Cinemachine.m_YAxis.Value += -TouchDist.y * Time.deltaTime * lookSpeed * YSenstivity;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;
        //Debug.Log("Pointer down");
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }

}