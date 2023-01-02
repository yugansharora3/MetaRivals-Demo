using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public GameObject EntryPrefab;
    public Transform Table;
    public GameObject PlayerStats;
    public void UpdateLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Scoreboard",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboard, OnLeaderboardError);

        var PlayerRequest = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "Scoreboard",
            MaxResultsCount = 1
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(PlayerRequest, OnLeaderboardAroundPlayer, OnLeaderboardError);
    }

    public void OnLeaderboard(GetLeaderboardResult result)
    {
        foreach(Transform item in Table)
        {
            Destroy(item.gameObject);
        }
        foreach(var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(EntryPrefab, Table);
            TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (item.Position + 1).ToString();
            if(item.DisplayName != null)
                texts[1].text = item.DisplayName;
            else
                texts[1].text = item.PlayFabId;
            texts[2].text = item.StatValue.ToString();
        }
    }
    public void OnLeaderboardAroundPlayer(GetLeaderboardAroundPlayerResult result)
    {
        foreach(var item in result.Leaderboard)
        {
            TextMeshProUGUI[] texts = PlayerStats.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (item.Position + 1).ToString();
            if(item.DisplayName != null)
                texts[1].text = item.DisplayName;
            else
                texts[1].text = item.PlayFabId;
            texts[2].text = item.StatValue.ToString();
        }
    }

    public void OnLeaderboardError(PlayFabError error)
    {
        Debug.Log("Leaderboard Error " + error.ToString());
    }
}
