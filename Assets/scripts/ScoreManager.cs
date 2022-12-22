using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class ScoreManager : MonoBehaviour
{
    public GameObject ScoreBoard;
    public int score = 0, TotalCoins = 0, MaxCoins = 20;

    private void Start()
    {
        UpdateScore();
    }
    public void UpdateScore()
    {
        if(ScoreBoard == null)
            ScoreBoard = GameObject.Find("Score").transform.GetChild(0).gameObject;
        TextMeshProUGUI Text = ScoreBoard.GetComponent<TextMeshProUGUI>();
        Text.text = "MRVL Points : " + score + "/" + MaxCoins;
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

    private void Update()
    {
    }
}
