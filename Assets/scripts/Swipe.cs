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
    private ShibaControls playerinput;
    void Start()
    {
        playerinput = new ShibaControls();
        GameObject canvas = GameObject.Find("Canvas");
        UIElements = new GameObject[4];
        for (int i = 0, j = 0; i < canvas.transform.childCount; i++)
        {
            if (canvas.transform.GetChild(i).gameObject.name != "Look")
            {
                UIElements[j] = canvas.transform.GetChild(i).gameObject;
                j++;
            }
        }
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

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            if (!CheckIfPointIsOnUI((Vector2)Input.mousePosition))
            {
                if (check())
                {
                    tap = true;
                    isDragging = true;
                    startTouch = Input.mousePosition;
                }

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
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                if(!CheckIfPointIsOnUI(Input.touches[0].position))
                {
                    if(check())
                    {
                        tap = true;
                        isDragging = true;
                        startTouch = Input.touches[0].position;
                    }
                    
                }
                
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled )
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
                if (!CheckIfPointIsOnUI(Input.touches[0].position))
                {
                    if(check())
                    {
                        swipeDelta = Input.touches[0].position - startTouch;

                    }
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (!CheckIfPointIsOnUI((Vector2)Input.mousePosition))
                {
                    if (check())
                    {
                        swipeDelta = (Vector2)Input.mousePosition - startTouch;

                    }
                }
            }

        }
                
        t += (5f * Time.deltaTime);
        float xval = Mathf.Lerp(0f, swipeDelta.x * 1 * lookSpeed * Time.deltaTime, t);
        cinemachine.m_XAxis.Value += xval;
        float yval = Mathf.Lerp(0f, -swipeDelta.y * 0.01f * lookSpeed * Time.deltaTime, t);
        //cinemachine.m_XAxis.Value += swipeDelta.x * 100 * lookSpeed * Time.deltaTime;
        cinemachine.m_YAxis.Value += yval;
        
        

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                if (!CheckIfPointIsOnUI(Input.touches[0].position))
                {
                    if (check())
                    {
                        tap = true;
                        isDragging = true;
                        startTouch = Input.touches[0].position;
                    }
                }
            }
        }

        if (t > 1.0f)
        {
            t = 0.0f;
            storedDelta = Vector2.zero;
        }
        Debug.Log("END-------------------------------------------------------------------------------");
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    private bool CheckIfPointIsOnUI(Vector2 position)
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 po = UIElements[i].GetComponent<Transform>().position;
            RectTransform rTransform = UIElements[i].GetComponent<RectTransform>();
            Rect r = rTransform.rect;
            Vector2 pos = rTransform.anchoredPosition;
            Debug.Log("position =" + position);
            Debug.Log("pos =" + pos);
            Debug.Log("height =" + r.height + " width = " + r.width);
            if(playerinput.PlayerMain.Move.ReadValue<Vector2>()!= Vector2.zero)
            {
                Debug.Log("Not Moving");
                return true;
            }
            if (position.x > 0 && position.x < 500f && position.y > 0f && position.y < 500f)
            {
                return true;
            }
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
