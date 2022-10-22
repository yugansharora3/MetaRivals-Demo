using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int RotateSpeed;
    void Start()
    {
        RotateSpeed = 2;
    }

    void Update()
    {
        transform.Rotate(0, RotateSpeed, 0, Space.World);
    }

}
