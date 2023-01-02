using System.IO;
using UnityEngine;
using Cinemachine;
using TMPro;
using PlayFab.ClientModels;
using PlayFab;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

public class ManagerScript : Singleton<ManagerScript>
{//Singleton<ManagerScript>
    public GameObject Ape;
    public GameObject Shiba;
    public GameObject Kishu;
    public GameObject WhiteWitch;
    public GameObject Volt;
    public GameObject CoinParent;
    bool ScoreSubmitted = false;
    public TextMeshProUGUI Inputfield,TotalPointsBoard;

    [HideInInspector]
    public TextMeshPro TopUsernameComponent;
    bool GotPlayerData = false;
    public struct PlayerData
    {
        public int GamesPlayed,TotalCoinsCollected;
        public string Username;
    }
    public PlayerData data;

    public void Start()
    {
        Ape.SetActive(false);
        Volt.SetActive(false);
        Shiba.SetActive(false);
        WhiteWitch.SetActive(false);
        Kishu.SetActive(false);
        
        SetCharacter();
        //UpdatePlayerData();
    }

    void SetCharacter()
    {
        string objectName = PlayerPrefs.GetString("Chosen-Character");
        if(objectName == null || objectName == "")
        {
            objectName = "volt";
        }
        if (objectName == "ape")
        {
            SetCharacterProperties(Ape);
        }
        if (objectName == "volt")
        {
            SetCharacterProperties(Volt);
        }
        if (objectName == "shiba")
        {
            SetCharacterProperties(Shiba);
        }
        if (objectName == "kishu")
        {
            SetCharacterProperties(Kishu);
        }
        if (objectName == "whiteWitch")
        {
            SetCharacterProperties(WhiteWitch);
        }
    }

    void SetCharacterProperties(GameObject obj)
    {
        obj.SetActive(true);
        GameObject cinemachine = GameObject.Find("CM FreeLook");
        cinemachine.GetComponent<CinemachineFreeLook>().Follow = obj.transform;
        cinemachine.GetComponent<CinemachineFreeLook>().LookAt = obj.transform;
        GameObject textObj = obj.transform.GetChild(obj.transform.childCount - 1).gameObject;
        TopUsernameComponent = textObj.GetComponent<TextMeshPro>();
    }

    void UpdatePlayerData()
    {
        var datarequest = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Games Played", (data.GamesPlayed).ToString() }
            }
        };
        PlayFabClientAPI.UpdateUserData(datarequest, (result) =>
        {
            Debug.Log(result.ToString());
        }, (error) =>
        {
            Debug.Log(error.ToString());
            Debug.Log("Updating Total Games Played data failed");
        });
    }
    void GetPlayerData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), GetDataSuccess,
        (error)=>
        { 
            Debug.Log("<color=red>Error: </color>" + error.ToString());
        });
    }

    void GetDataSuccess(GetUserDataResult result)
    {
        if (result.Data.ContainsKey("Games Played"))
            data.GamesPlayed = int.Parse(result.Data["Games Played"].Value) + 1;
        if (result.Data.ContainsKey("Total Coins Collected"))
        {
            data.TotalCoinsCollected = int.Parse(result.Data["Total Coins Collected"].Value);
            TotalPointsBoard.text = data.TotalCoinsCollected + " TAMP";
            Debug.Log(TotalPointsBoard.text);
        }
        
        UpdatePlayerData();
    }

    public void ChangeUserName()
    {
        TopUsernameComponent.text = "@" + Inputfield.text;
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = Inputfield.text
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, (result)=>
        {
            Debug.Log(result);
        }, (error)=>
        {
            Debug.Log(error);
        });
    }

    public bool CheckIfCoinIsActive()
    {
        for (int i = 0; i < CoinParent.transform.childCount; i++)
        {
            if(CoinParent.transform.GetChild(i).gameObject.activeSelf) return true;
        }
        return false;
    }

    public void Update()
    {
        if(!CheckIfCoinIsActive() && !ScoreSubmitted)
        {
            GetComponent<ScoreManager>().SubmitScore();
            ScoreSubmitted = true;
        }
        if(PlayerPrefs.GetString("UserName") != null && PlayerPrefs.GetString("UserName") != "")
        {
            data.Username = TopUsernameComponent.text = PlayerPrefs.GetString("UserName");
        }
        if(PlayFabClientAPI.IsClientLoggedIn() && !GotPlayerData)
        {
            GetPlayerData();
            GotPlayerData = true;
        }
    }
}
