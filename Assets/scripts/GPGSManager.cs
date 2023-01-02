using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using TMPro;

public class GPGSManager : MonoBehaviour
{
    PlayGamesClientConfiguration clientConfiguration;
    public TextMeshProUGUI StatusText;
    // Start is called before the first frame update
    void Awake()
    {
        clientConfiguration = new PlayGamesClientConfiguration.Builder()
            .AddOauthScope("profile")
            .RequestServerAuthCode(false)
            .Build();
        SignInGPGS(SignInInteractivity.CanPromptAlways, clientConfiguration);
        Debug.Log("LOL");
        if (GetComponent<PlayfabManagar>() == null)
            Debug.Log("null");
        else
            GetComponent<PlayfabManagar>().LogIn();
    }
    internal void SignInGPGS(SignInInteractivity interactivity,PlayGamesClientConfiguration configuration)
    {
        configuration = clientConfiguration;
        StatusText.text = "Sign in started";
        PlayGamesPlatform.InitializeInstance(configuration);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        StatusText.text = "Initialized";
        PlayGamesPlatform.Activate();
        StatusText.text = "Activated";
        PlayGamesPlatform.Instance.Authenticate((code,message) =>
        {
            Debug.Log("Authenticate...");
            StatusText.text = "Authenticate...";
            if (code)// == SignInStatus.Success)
            {
                StatusText.text = "Successfully Authenticated " + Social.localUser.userName;
                Debug.Log("Successfully Authenticated");
                Debug.Log(Social.localUser.userName);
            }
            else
            {
                StatusText.text = "Failed to authenticate" + code.ToString() + message;
                Debug.Log("Failed to authenticate");
            }
        });
    }

    public void SignIn()
    {
        SignInGPGS(SignInInteractivity.CanPromptAlways, clientConfiguration);
    }
    public void SignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
        StatusText.text = "Signed out from " + Social.localUser.userName;
    }

}
