using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInfo : MonoBehaviour
{
    public int PlatformNumber;
    public Vector3 AdditionalPosition;

    public void Init(int platformNumber, Vector3 additionalPosition)
    {
        PlatformNumber = platformNumber;
        AdditionalPosition = additionalPosition;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
