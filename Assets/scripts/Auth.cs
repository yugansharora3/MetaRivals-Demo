using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FusedVR.Crypto;

public class Auth : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        ChainAuthManager instance = await ChainAuthManager.Register(SystemInfo.deviceUniqueIdentifier, "WmADvfZuQI");
        Debug.LogError(instance.RegisterCode);
        if(await instance.AwaitLogin())
        {
            Debug.LogError("Success");
        }
        else
        {
            Debug.LogError("Not authenticated");
        }
    }

}
