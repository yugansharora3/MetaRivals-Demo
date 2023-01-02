using GooglePlayGames;
using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugPlayfab : MonoBehaviour
{
    public TextMeshProUGUI GoogleStatusText;
    // Start is called before the first frame update
    public void Awake()
    {
        if(!PlayFabClientAPI.IsClientLoggedIn())
            LogIn();
    }

    public void LogIn()
    {
        var request = new LoginWithAndroidDeviceIDRequest
        {
            AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLoginSuccess, OnLoginFailure);
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Success Login");
        GoogleStatusText.text = "Success Login";
        if (result.InfoResultPayload.PlayerProfile.DisplayName != null)
        {
            PlayerPrefs.SetString("UserName", result.InfoResultPayload.PlayerProfile.DisplayName);
        }
    }

    void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("Login Fail");
        GoogleStatusText.text = "Login Fail";
    }
}
