using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
   [SerializeField] private TMP_Text coinText;
   [SerializeField] private TMP_Text noAdsText;
   [SerializeField] private TMP_Text premiumText;
   [SerializeField] private TMP_Text versionText;

   private void Start()
   {
      UpdateUI();
      versionText.text = GetVersionCode();
   }

   public void UpdateReward(IAPProductKey key)
   {
      switch (key)
      {
         case IAPProductKey.Coin100:
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins",0) + 100);
            break;
         case IAPProductKey.Coin200:
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins",0) + 200);
            break;
         case IAPProductKey.NoAds:
            PlayerPrefs.SetInt("no_ads", PlayerPrefs.GetInt("no_ads",0) == 0 ? 1 : 0);
            break;
         case IAPProductKey.PremiumMonth:
            PlayerPrefs.SetInt("premium", PlayerPrefs.GetInt("premium",0) == 0 ? 1 : 0 );
            break;
         default:
            break;
      }

      UpdateUI();
   }

   private void UpdateUI()
   {
      coinText.text = PlayerPrefs.GetInt("coins", 0).ToString();
      noAdsText.text = PlayerPrefs.GetInt("no_ads", 0) == 0 ? "false" : "true";
      premiumText.text = PlayerPrefs.GetInt("premium", 0) == 0 ? "false" : "true";
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
}
