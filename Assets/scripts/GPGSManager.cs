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
    public TextMeshProUGUI Statustext;
    // Start is called before the first frame update
    void Awake()
    {
        clientConfiguration = new PlayGamesClientConfiguration.Builder()
            .AddOauthScope("profile")
            .RequestServerAuthCode(false)
            .Build();
        SignInGPGS(SignInInteractivity.CanPromptOnce,clientConfiguration);
    }
    internal void SignInGPGS(SignInInteractivity interactivity,PlayGamesClientConfiguration configuration)
    {
        configuration = clientConfiguration;
        Statustext.text = "Sign in started";
        PlayGamesPlatform.InitializeInstance(configuration);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        Statustext.text = "Initialized";
        PlayGamesPlatform.Activate();
        Statustext.text = "Activated";
        PlayGamesPlatform.Instance.Authenticate((code,message) =>
        {
            Debug.Log("Authenticate...");
            Statustext.text = "Authenticate...";
            if (code)// == SignInStatus.Success)
            {
                Statustext.text = "Successfully Authenticated " + Social.localUser.userName;
                Debug.Log("Successfully Authenticated");
                Debug.Log(Social.localUser.userName);
            }
            else
            {
                Statustext.text = "Failed to authenticate" + code.ToString() + message;
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
        Statustext.text = "Signed out from " + Social.localUser.userName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
