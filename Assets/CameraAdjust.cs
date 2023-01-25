using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjust : MonoBehaviour
{
    public Camera HeadCamera;
    // Start is called before the first frame update
    void Start()
    {
        HeadCamera.farClipPlane = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
