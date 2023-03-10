using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class ScoreManager : MonoBehaviour
{
    public GameObject ScoreBoardText;
    public int score = 0, TotalCoins = 0, MaxCoins = 20;

    private void Start()
    {
        UpdateScore();
    }
    public void UpdateScore()
    {
        if(ScoreBoardText == null)
            ScoreBoardText = GameObject.Find("Score").transform.GetChild(0).gameObject;
        TextMeshProUGUI Text = ScoreBoardText.GetComponent<TextMeshProUGUI>();
        Text.text = score + " POINTS";
    }
    public void IncreaseScore()
    {
        score++;
    }
    public void SubmitScore()
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Scoreboard",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnScoreSubmitSuccess, OnError);

        int TotalCoinsCollected = GetComponent<ManagerScript>().data.TotalCoinsCollected;

        var datarequest = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Total Coins Collected", (score + TotalCoinsCollected).ToString() }
            }
        };
        PlayFabClientAPI.UpdateUserData(datarequest, (result) =>
        {
            Debug.Log(result);
        }, (error) =>
        {
            Debug.Log(error);
            Debug.Log("Updating Total Coins data failed");
        });
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error);
        Debug.Log("Score Submit Fail");
    }

    void OnScoreSubmitSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Score Submit success");
    }

}
