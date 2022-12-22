using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;

public class PlayfabManagar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LogIn();
    }

    void LogIn()
    {
        var request = new LoginWithAndroidDeviceIDRequest
        {
            AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithAndroidDeviceID(request,OnLoginSuccess,OnLoginFailure);
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Sucess Login");
    }
    
    void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("Login Fail");
    }

    
}
