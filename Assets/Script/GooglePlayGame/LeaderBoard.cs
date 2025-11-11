using System;
using System.Collections;
using System.Collections.Generic;
using Constant;
using GooglePlayGames;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private TMP_InputField txtScore;
    [SerializeField] private TMP_Text txtError;
    
    private void ShowLeaderboard()
    {
        if (Social.localUser.authenticated) PlayGamesPlatform.Instance.ShowLeaderboardUI(id.leaderboard_scores);
        else txtError.text = "User not signed in to Google Play Games.";
    }

    public void UpdateScoreButtonClicked()
    {
        try
        {
            var zws = char.ConvertFromUtf32(8203);
            var clean = txtScore.text.Replace(zws, "");
            
            if (int.TryParse(clean, out int sc)) PostScore(sc);
            else txtError.text = $"{clean} - invalid number format";
        }
        catch (Exception e)
        {
            txtError.text = $"{txtScore.text} - {e.Message}";
        }
    }

    public void ShowLeaderBoard()
    {
        ShowLeaderboard();
    }

    public void Home()
    {
        SceneManager.LoadScene("Login");
    }

    private void PostScore(long score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, id.leaderboard_scores, success =>
            {
                Debug.Log(success ? "Score posted!" : "Failed to post score.");
                txtError.text = success ? "Score posted!" : "Failed to post score.";

            });
        }
        else
        {
            Debug.LogWarning("User not signed in to Google Play Games.");
            txtError.text = "User not signed in to Google Play Games.";
        }
    }
}
