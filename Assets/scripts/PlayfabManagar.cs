using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PlayFab;
using GooglePlayGames;
using TMPro;
using PlayFabError = PlayFab.PlayFabError;
using LoginResult = PlayFab.ClientModels.LoginResult;

public class PlayfabManagar : MonoBehaviour
{
    public TextMeshProUGUI GoogleStatusText;
    public Player data;
    // Start is called before the first frame update
    void Start()
    {
        //LogIn();
    } 
    public void GoogleSignInWithPlayfab()
    {
        Debug.Log("GoogleSignInWithPlayfab called");
        if(PlayGamesPlatform.Instance.IsAuthenticated())
        {
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
        else
        {
            bool value = EditorUtility.DisplayDialog("Play", "Google Play Games Sign in failed \n Ensure that you have google play games on your device", "Exit");
            if(value)
            {
                //Exit the game 
                Application.Quit();
            }
        }
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
                {"gmail id", ((PlayGamesLocalUser)PlayGamesPlatform.Instance.localUser).Email }
            }
        };
        if (data.GamesPlayed != 0)
        {
            datarequest.Data.Add("Games Played", data.GamesPlayed.ToString());
            datarequest.Data.Add("Total Coins Collected", data.TotalCoinsCollected.ToString());
        }
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
        
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Success Login");
        GoogleStatusText.text = "Success Login";
        if (result.InfoResultPayload.PlayerProfile.DisplayName != null)
        {
            PlayerPrefs.SetString("UserName", result.InfoResultPayload.PlayerProfile.DisplayName);
        }
        GoogleSignInWithPlayfab();
        ////Get the player data to check if this device
        //GetPlayerData();
    }
    void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("Login Fail");
        GoogleStatusText.text = "Login Fail";
    }
}
