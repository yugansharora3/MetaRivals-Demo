using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaturnRotate : MonoBehaviour
{
    public float RotateSpeed;
    public float SkyRotateSpeed;

    void Update()
    {
        transform.Rotate(0, RotateSpeed, 0, Space.Self);
        RenderSettings.skybox.SetFloat("_Rotation",Time.time * SkyRotateSpeed);
    }
}
