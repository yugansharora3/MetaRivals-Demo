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
        //LogIn();
    }
    public void GoogleSignInWithPlayfab()
    {
        Debug.Log("GoogleSignInWithPlayfab called");
        Social.localUser.Authenticate((bool success) => 
        {
            if (success)
            {
                GoogleStatusText.text = "Google Signed In";
                var serverAuthCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                GoogleStatusText.text = "Server Auth Code: " + serverAuthCode;
                Debug.Log("Server Auth Code: " + serverAuthCode);

                var request = new LoginWithGoogleAccountRequest()
                {
                    TitleId = PlayFabSettings.TitleId,
                    ServerAuthCode = serverAuthCode,
                    CreateAccount = true,
                    InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
                    {
                        GetPlayerProfile = true
                    }
                };

                PlayFabClientAPI.LoginWithGoogleAccount(request, OnPlayfabGoogleLoginSuccess, OnPlayfabGoogleLoginError);
            }
            else
            {
                GoogleStatusText.text = "Google Failed to Authorize your login";
            }

        });

    }
    void OnPlayfabGoogleLoginSuccess(LoginResult result)
    {
        GoogleStatusText.text = "Signed In as " + result.PlayFabId;
        Debug.Log("Signed In as " + result.PlayFabId);
        if (result.InfoResultPayload.PlayerProfile.DisplayName != null)
        {
            PlayerPrefs.SetString("UserName", result.InfoResultPayload.PlayerProfile.DisplayName);
        }
        var datarequest = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"gmail id", ((PlayGamesLocalUser)Social.localUser).Email }
            }
        };

        Debug.Log(((PlayGamesLocalUser)PlayGamesPlatform.Instance.localUser).Email);

        Debug.Log("<color=red>Error: </color>" + ((PlayGamesLocalUser)Social.localUser).Email);
        Debug.Log("<color=red>Error: </color>" + ((PlayGamesLocalUser)Social.localUser).AvatarURL);
        PlayFabClientAPI.UpdateUserData(datarequest, (result) =>
        {
            Debug.Log(result.ToString());
        }, (error) =>
        {
            Debug.Log(error.ToString());
            Debug.Log("Updating Total Games Played data failed");
        });
    }
    
    void OnPlayfabGoogleLoginError(PlayFabError error)
    {
        GoogleStatusText.text = error.ToString();
        Debug.Log("Login Fail" + error);
    }
    public void LogIn()
    {
        Debug.Log("Called Login");
        var request = new LoginWithAndroidDeviceIDRequest
        {
            AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithAndroidDeviceID(request,OnLoginSuccess,OnLoginFailure);
        GoogleSignInWithPlayfab();
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
