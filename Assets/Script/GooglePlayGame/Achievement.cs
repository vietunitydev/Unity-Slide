using Constant;
using GooglePlayGames;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.GooglePlayGame
{
    public class Achievement : MonoBehaviour
    {
        [SerializeField] private bool activatePgsIfNeeded = true;
        [SerializeField] private TMP_Text detailText;

        void Awake()
        {
            if (activatePgsIfNeeded)
                PlayGamesPlatform.Activate();
        }

        public void UpdateLevel(int level)
        {
            if (level >= 10) Unlock(id.achievement_reach_level_10);
            if (level >= 20) Unlock(id.achievement_reach_level_20);
        }

        public void Unlock(string achievementId)
        {
            Social.ReportProgress(achievementId, 100.0f, success =>
            {
                Debug.Log(success ? $"[PGS] Unlocked: {achievementId}" : $"[PGS] Unlock failed: {achievementId}");
                detailText.text = success ? $"[PGS] Unlocked: {achievementId}" : $"[PGS] Unlock failed: {achievementId}";
            });
        }

        public void Increment(string achievementId, int steps = 1)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(achievementId, steps, success =>
            {
                Debug.Log(success ? $"[PGS] Incremented {achievementId} by {steps}" : $"[PGS] Increment failed: {achievementId}");
                detailText.text = success ? $"[PGS] Incremented {achievementId} by {steps}" : $"[PGS] Increment failed: {achievementId}";
            });
        }
    
        public void ShowAchievementsUI()
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }

        public void Login()
        {
            SceneManager.LoadScene("Login");
        }
    }
}
