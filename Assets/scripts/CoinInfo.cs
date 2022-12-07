using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInfo : MonoBehaviour
{
    public GameObject Platform;
    public int PlatformNumber;
    public Vector3 PlatformLocalPosition,PlatformPosition, AdditionalPosition,extents,length,CoinLocal,CoinPos;

    public void Init(GameObject platform, int platformNumber, Vector3 additionalPosition)
    {
        Platform = platform;
        PlatformNumber = platformNumber;
        AdditionalPosition = additionalPosition;
        extents = platform.GetComponent<MeshFilter>().mesh.bounds.extents;
        PlatformPosition = Platform.transform.position;
        PlatformLocalPosition = Platform.transform.localPosition;
        length = Vector3.Scale(Platform.GetComponent<MeshFilter>().mesh.bounds.extents, Platform.transform.localScale);
        CoinLocal = transform.localPosition; 
        CoinPos = transform.position;
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
