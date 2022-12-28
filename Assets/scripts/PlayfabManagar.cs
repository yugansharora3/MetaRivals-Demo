using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using GooglePlayGames;
using TMPro;
using PlayFabError = PlayFab.PlayFabError;
using LoginResult = PlayFab.ClientModels.LoginResult;

public class PlayfabManagar : MonoBehaviour
{
    public TextMeshProUGUI GoogleStatusText;
    // Start is called before the first frame update
    void Start()
    {
        LogIn();
    }
    public void GoogleSignIn()
    {
        Social.localUser.Authenticate((bool success) => {

            if (success)
            {
                GoogleStatusText.text = "Google Signed In";
                var serverAuthCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                Debug.Log("Server Auth Code: " + serverAuthCode);

                PlayFabClientAPI.LoginWithGoogleAccount(new LoginWithGoogleAccountRequest()
                {
                    TitleId = PlayFabSettings.TitleId,
                    ServerAuthCode = serverAuthCode,
                    CreateAccount = true
                }, (result) =>
                {
                    GoogleStatusText.text = "Signed In as " + result.PlayFabId;

                }, OnPlayFabError);
            }
            else
            {
                GoogleStatusText.text = "Google Failed to Authorize your login";
            }

        });

    }
    void OnPlayFabError(PlayFabError error)
    {
        Debug.Log("Login Fail");
    }
    public void LogIn()
    {
        var request = new LoginWithAndroidDeviceIDRequest
        {
            AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithAndroidDeviceID(request,OnLoginSuccess,OnLoginFailure);
        //SignIn();
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Success Login");
        GoogleStatusText.text = "Success Login";
    }
    
    void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("Login Fail");
        GoogleStatusText.text = "Login Fail";
    }
}
