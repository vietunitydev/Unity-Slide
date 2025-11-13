using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class PlayfabLogin : MonoBehaviour
{
    [SerializeField] private GameObject profile;
    [SerializeField] private TMP_Text basicInfoText;

    [Header("LoginWithCustomId")]
    [SerializeField] private GameObject customIdArea;
    [SerializeField] private TMP_InputField customIdText;
    
    [Header("LoginWithUsernamePassword")]
    [SerializeField] private GameObject usernamePasswordArea;
    [SerializeField] private TMP_InputField userNameText;
    [SerializeField] private TMP_InputField passwordText;
    
    [Header("LoginWithEmailPassword")]
    [SerializeField] private GameObject emailPasswordArea;
    [SerializeField] private TMP_InputField emailText;
    [SerializeField] private TMP_InputField emailPassText;

    private void Start()
    {
        profile.SetActive(false);
        basicInfoText.text = "";
        
        customIdArea.SetActive(false);
        usernamePasswordArea.SetActive(false);
        emailPasswordArea.SetActive(false);
    }

    public void ShowCustomIdText()
    {
        customIdArea.SetActive(!customIdArea.activeSelf);
        usernamePasswordArea.SetActive(false);
        emailPasswordArea.SetActive(false);
    }
    
    public void ShowUsernamePasswordText()
    {
        usernamePasswordArea.SetActive(!usernamePasswordArea.activeSelf);
        customIdArea.SetActive(false);
        emailPasswordArea.SetActive(false);
    }
    
    public void ShowEmailPasswordText()
    {
        emailPasswordArea.SetActive(!emailPasswordArea.activeSelf);
        usernamePasswordArea.SetActive(false);
        customIdArea.SetActive(false);
    }

    public void OnLoginWithDeviceIdClicked()
    {
        var request = new LoginWithCustomIDRequest()
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginError);
        
        usernamePasswordArea.SetActive(false);
        customIdArea.SetActive(false);
    }
    
    public void OnLoginWithCustomIdClicked()
    {
        if(customIdText.text == string.Empty)  
            return;
        LoginWithCustomId(customIdText.text);
    }
    
    public void OnLoginWithUsernameClicked()
    {
        if(userNameText.text == string.Empty)  
            return;
        if(passwordText.text == string.Empty)  
            return;
        LoginWithUsername(userNameText.text, passwordText.text);
    }
    
    public void OnLoginWithEmailClicked()
    {
        if(emailText.text == string.Empty)  
            return;
        if(emailPassText.text == string.Empty)  
            return;
        LoginWithEmail(emailText.text, emailPassText.text);
    }

    private void LoginWithCustomId(string customId)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginError);
    }

    private void LoginWithEmail(string email, string password)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginError);
    }

    private void LoginWithUsername(string username, string password)
    {
        var request = new LoginWithPlayFabRequest
        {
            Username = username,
            Password = password
        };

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginError);
    }
    
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");
        profile.SetActive(true);
        basicInfoText.text =
            $"Login OK\n" +
            $"PlayFabId: {result.PlayFabId}\n" +
            $"NewlyCreated: {result.NewlyCreated}";
    }
    
    private void OnLoginError(PlayFabError error)
    {
        Debug.Log("Error: " + error.GenerateErrorReport());
    }
    
    
    public void LogoutPlayFab()
    {
        PlayFabClientAPI.ForgetAllCredentials();
    }
    
    // // =========================================================
    // //          CÁC HÀM LẤY THÔNG TIN NGƯỜI CHƠI
    // // =========================================================
    //
    // /// <summary>
    // /// Gọi 1 lần sau khi login thành công để lấy mọi thông tin.
    // /// </summary>
    // public void LoadAllUserInfo()
    // {
    //     GetAccountInfo();
    //     GetPlayerProfile();
    //     GetPlayerStatistics();
    //     GetUserData();
    // }
    //
    // /// <summary>
    // /// Lấy thông tin tài khoản: Email, Username, DisplayName, Country, Origination...
    // /// </summary>
    // private void GetAccountInfo()
    // {
    //     var request = new GetAccountInfoRequest();
    //
    //     PlayFabClientAPI.GetAccountInfo(request,
    //         result =>
    //         {
    //             var info = result.AccountInfo;
    //
    //             string username    = info.Username;
    //             string email       = info.PrivateInfo?.Email;
    //             string displayName = info.TitleInfo?.DisplayName;
    //             string origination = info.Origination.ToString();
    //             string country     = info.Location?.CountryCode.ToString();
    //
    //             Debug.Log($"[AccountInfo] Username: {username}, Email: {email}, DisplayName: {displayName}, Country: {country}, Origination: {origination}");
    //
    //             if (accountInfoText != null)
    //             {
    //                 accountInfoText.text =
    //                     $"[AccountInfo]\n" +
    //                     $"Username: {username}\n" +
    //                     $"Email: {email}\n" +
    //                     $"DisplayName: {displayName}\n" +
    //                     $"Country: {country}\n" +
    //                     $"Origination: {origination}";
    //             }
    //         },
    //         OnPlayFabCommonError);
    // }
    //
    // /// <summary>
    // /// Lấy profile người chơi: DisplayName, AvatarUrl, Location...
    // /// (một số thứ trùng với AccountInfo nhưng đôi khi dùng view constraints khác)
    // /// </summary>
    // private void GetPlayerProfile()
    // {
    //     var request = new GetPlayerProfileRequest
    //     {
    //         ProfileConstraints = new PlayerProfileViewConstraints
    //         {
    //             ShowDisplayName = true,
    //             ShowAvatarUrl   = true,
    //             ShowLocations   = true,
    //             ShowCreated     = true,
    //             ShowLastLogin   = true
    //         }
    //     };
    //
    //     PlayFabClientAPI.GetPlayerProfile(request,
    //         result =>
    //         {
    //             var profile = result.PlayerProfile;
    //
    //             if (profile == null)
    //             {
    //                 Debug.LogWarning("[PlayerProfile] null");
    //                 return;
    //             }
    //
    //             string displayName = profile.DisplayName;
    //             string avatarUrl   = profile.AvatarUrl;
    //             string country     = (profile.Locations != null && profile.Locations.Count > 0)
    //                 ? profile.Locations[0].CountryCode.ToString()
    //                 : "Unknown";
    //
    //             Debug.Log($"[PlayerProfile] DisplayName: {displayName}, Avatar: {avatarUrl}, Country: {country}");
    //
    //             // Nếu bạn muốn hiển thị thêm, có thể nối vào accountInfoText hoặc UI khác
    //             if (accountInfoText != null)
    //             {
    //                 accountInfoText.text +=
    //                     $"\n\n[Profile]\n" +
    //                     $"DisplayName: {displayName}\n" +
    //                     $"AvatarUrl: {avatarUrl}\n" +
    //                     $"Country(Profile): {country}";
    //             }
    //         },
    //         OnPlayFabCommonError);
    // }
    //
    // /// <summary>
    // /// Lấy thống kê (stats): TopScore, Level, Coins...
    // /// Bạn set tên StatisticName gì trong UpdatePlayerStatistics thì ghi đúng y như vậy.
    // /// </summary>
    // private void GetPlayerStatistics()
    // {
    //     var request = new GetPlayerStatisticsRequest
    //     {
    //         // Nếu null hoặc không set, PlayFab trả về toàn bộ stats.
    //         // Có thể specify để tối ưu:
    //         StatisticNames = new List<string> { "TopScore", "Level", "Coins" }
    //     };
    //
    //     PlayFabClientAPI.GetPlayerStatistics(request,
    //         result =>
    //         {
    //             int topScore = 0;
    //             int level    = 0;
    //             int coins    = 0;
    //
    //             foreach (var stat in result.Statistics)
    //             {
    //                 Debug.Log($"[Stats] {stat.StatisticName}: {stat.Value}");
    //
    //                 switch (stat.StatisticName)
    //                 {
    //                     case "TopScore":
    //                         topScore = stat.Value;
    //                         break;
    //                     case "Level":
    //                         level = stat.Value;
    //                         break;
    //                     case "Coins":
    //                         coins = stat.Value;
    //                         break;
    //                 }
    //             }
    //
    //             if (statsInfoText != null)
    //             {
    //                 statsInfoText.text =
    //                     $"[Statistics]\n" +
    //                     $"TopScore: {topScore}\n" +
    //                     $"Level: {level}\n" +
    //                     $"Coins: {coins}";
    //             }
    //         },
    //         OnPlayFabCommonError);
    // }
    //
    // /// <summary>
    // /// Lấy custom user data (Coin, Skin, Exp...). 
    // /// Bạn tự tạo key trong PlayFab hoặc bằng UpdateUserData.
    // /// </summary>
    // private void GetUserData()
    // {
    //     var request = new GetUserDataRequest
    //     {
    //         // Nếu Keys = null hoặc không set → trả về tất cả key.
    //         // Có thể giới hạn: Keys = new List<string>{ "Coin", "Exp", "SelectedSkin" }
    //     };
    //
    //     PlayFabClientAPI.GetUserData(request,
    //         result =>
    //         {
    //             if (result.Data == null || result.Data.Count == 0)
    //             {
    //                 Debug.Log("[UserData] No custom data");
    //                 if (dataInfoText != null)
    //                     dataInfoText.text = "[UserData]\nKhông có dữ liệu.";
    //                 return;
    //             }
    //
    //             // Ví dụ đọc 3 key phổ biến
    //             string coin       = result.Data.ContainsKey("Coin")        ? result.Data["Coin"].Value        : "N/A";
    //             string exp        = result.Data.ContainsKey("Exp")         ? result.Data["Exp"].Value         : "N/A";
    //             string selectedSK = result.Data.ContainsKey("SelectedSkin")? result.Data["SelectedSkin"].Value: "N/A";
    //
    //             Debug.Log($"[UserData] Coin: {coin}, Exp: {exp}, Skin: {selectedSK}");
    //
    //             if (dataInfoText != null)
    //             {
    //                 dataInfoText.text =
    //                     "[UserData]\n" +
    //                     $"Coin: {coin}\n" +
    //                     $"Exp: {exp}\n" +
    //                     $"SelectedSkin: {selectedSK}\n\n" +
    //                     "Tất cả key:\n";
    //
    //                     foreach (var kvp in result.Data)
    //                         dataInfoText.text += $"{kvp.Key}: {kvp.Value.Value}\n";
    //             }
    //         },
    //         OnPlayFabCommonError);
    // }
    //
    // /// <summary>
    // /// Hàm xử lý lỗi chung cho các request info.
    // /// </summary>
    // private void OnPlayFabCommonError(PlayFabError error)
    // {
    //     Debug.LogError("[PlayFab Error] " + error.GenerateErrorReport());
    // }
}
