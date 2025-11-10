using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;

public class PlayGameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text detailText;
    // Start is called before the first frame update
    private int i = 0;
    private void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        SignIn();
    }

    public void SignIn()
    {
        i++;
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    private void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            var userName = PlayGamesPlatform.Instance.GetUserDisplayName();
            var id = PlayGamesPlatform.Instance.GetUserId();
            var imgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();
            
            Debug.Log($"User Detail : {userName} {id} {imgUrl}");
            detailText.text = $"User Detail : {userName} {id} {imgUrl}";
        }
        else
        {
            Debug.Log($"Sign in failed.... {i}");
            detailText.text = $"Sign in failed.... {i}";
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }
}
