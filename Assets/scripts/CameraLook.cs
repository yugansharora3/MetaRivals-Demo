using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraLook : MonoBehaviour
{
    [SerializeField]
    private float lookSpeed = 1f;

    private Vector2 prev;

    private CinemachineFreeLook cinemachine;
    private ShibaControls playerInput;

    private void Awake()
    {
        playerInput = new ShibaControls();
        cinemachine = GetComponent<CinemachineFreeLook>();
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = playerInput.PlayerMain.Look.ReadValue<Vector2>();
        
        cinemachine.m_XAxis.Value += delta.x * 100 * lookSpeed * Time.deltaTime;
        //if(cinemachine.m_YAxis.Value > 0.45 || -delta.y > 0)
            cinemachine.m_YAxis.Value += -delta.y * lookSpeed * Time.deltaTime;
    }
}
