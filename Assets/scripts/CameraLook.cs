using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraLook : MonoBehaviour
{
    [SerializeField]
    private float lookSpeed = 1f;

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
        cinemachine.m_XAxis.Value += delta.x * 50 * lookSpeed * Time.deltaTime;
        //cinemachine.m_YAxis.Value += delta.y * lookSpeed * Time.deltaTime;
    }
}
