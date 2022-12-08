using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

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

    // Use this for initialization
    void Start()
    {
        //cinemachine = Cinemachine.GetComponent<CinemachineFreeLook>();
        
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
                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }
        }
        else
        {
            TouchDist = new Vector2();
        }
        Cinemachine.m_XAxis.Value += TouchDist.x * Time.deltaTime * lookSpeed * XSenstivity;
        Cinemachine.m_YAxis.Value += TouchDist.y * Time.deltaTime * lookSpeed * YSenstivity;
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

}