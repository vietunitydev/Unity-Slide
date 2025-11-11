using Constant;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text detailText;
    [SerializeField] private TMP_Text versionText;
    [SerializeField] private Button btnLeaderboard;
    [SerializeField] private Button btnAchievement;
    // Start is called before the first frame update
    private int i = 0;
    private void Start()
    {
        btnLeaderboard.gameObject.SetActive(false);
        btnAchievement.gameObject.SetActive(false);
        versionText.text = GetVersionCode();
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
            detailText.text = $"User Detail : \n username: {userName} \n id: {id} \n url image: {imgUrl}";
            
            btnLeaderboard.gameObject.SetActive(true);
            btnAchievement.gameObject.SetActive(true);
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
    
    public void OnManualAuth()
    {
        i++;
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }

    private string GetVersionCode()
    {
        string versionName = Application.version;
        string buildCode = "unknown";

#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager"))
            using (var packageInfo = packageManager.Call<AndroidJavaObject>("getPackageInfo",
                    currentActivity.Call<string>("getPackageName"), 0))
            {
                int versionCode = packageInfo.Get<int>("versionCode");
                buildCode = versionCode.ToString();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Cannot get Android versionCode: " + e.Message);
        }
#elif UNITY_IOS && !UNITY_EDITOR
        buildCode = GetiOSBuildNumber();
#endif

        Debug.Log($"Version Name: {versionName} | Build Code: {buildCode}");
        return $"Version Name: {versionName} | Build Code: {buildCode}";
    }

#if UNITY_IOS && !UNITY_EDITOR
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern string _GetBuildNumber();

    private static string GetiOSBuildNumber()
    {
        return _GetBuildNumber();
    }
#endif

    public void MoveToLeaderBoard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
    public void MoveToAchievement()
    {
        SceneManager.LoadScene("Achievement");
    }

}
