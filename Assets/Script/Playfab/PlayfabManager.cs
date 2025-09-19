using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayfabManager : MonoBehaviour
{
    public LeaderBoardElement element;
    public Transform parent;
    private void Start()
    {
        Login();
    }

    private void Login()
    {
        var requets = new LoginWithCustomIDRequest()
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        
        PlayFabClientAPI.LoginWithCustomID(requets, OnSuccess, OnError);
    }

    private void UpdateLeaderboard(int score)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate { StatisticName = "TopScore", Value = score }
                }
            },
            result => Debug.Log("Successfully updated HighScore!"),
            error => Debug.LogError("Error updating statistic: " + error.GenerateErrorReport()));

    }

    private void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");
        // UpdateLeaderboard(2500);
        // GetLeaderBoard();
    }
    
    private void OnError(PlayFabError error)
    {
        Debug.Log("Error: " + error.GenerateErrorReport());
    }

    private void GetLeaderBoard()
    {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
            {
                StatisticName = "TopScore",
                StartPosition = 0, 
                MaxResultsCount = 10
            },
            result =>
            {
                foreach (var entry in result.Leaderboard)
                {
                    var e = Instantiate(element,parent);
                    e.gameObject.SetActive(true);
                    e.pName.text = entry.PlayFabId;
                    e.pScore.text = entry.StatValue.ToString();
                }
            },
            error => Debug.LogError("Error getting leaderboard: " + error.GenerateErrorReport()));
    }
}
