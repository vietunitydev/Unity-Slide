using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class PlayfabLeaderboard : MonoBehaviour
{
   
    public LeaderBoardElement element;
    public Transform parent;
    
    public TMP_InputField scoreInput;
    
    public void OnClickSubmitScore()
    {
        if (int.TryParse(scoreInput.text, out int score))
        {
            UpdateLeaderboard(score);
        }
        else
        {
            Debug.LogError("Error parse to int !");
        }
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

    public void GetLeaderBoard()
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
        
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
