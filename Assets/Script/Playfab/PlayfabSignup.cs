using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class PlayfabSignup : MonoBehaviour
{
    [Header("SignUpUserName")]
    [SerializeField] private GameObject usernamePasswordArea;
    [SerializeField] private TMP_InputField userNameText1;
    [SerializeField] private TMP_InputField passwordText1;
    
    [Header("SignupEmail")]
    [SerializeField] private GameObject emailArea;
    [SerializeField] private TMP_InputField emailText1;
    [SerializeField] private TMP_InputField emailPassText1;
    
    [Header("SignupEmailUserName")]
    [SerializeField] private GameObject emailusernameArea;
    [SerializeField] private TMP_InputField userNameText;
    [SerializeField] private TMP_InputField emailText;
    [SerializeField] private TMP_InputField emailPassText;

    private void Start()
    {
        emailArea.SetActive(false);
        usernamePasswordArea.SetActive(false);
        emailusernameArea.SetActive(false);
    }

    public void ShowEmailArea()
    {
        emailArea.SetActive(!emailArea.activeSelf);
        usernamePasswordArea.SetActive(false);
        emailusernameArea.SetActive(false);
    }

    public void ShowUserNameArea()
    {
        usernamePasswordArea.SetActive(!usernamePasswordArea.activeSelf);
        emailArea.SetActive(false);
        emailusernameArea.SetActive(false);
    }

    public void ShowEmailUserNameArea()
    {
        emailusernameArea.SetActive(!emailusernameArea.activeSelf);
        emailArea.SetActive(false);
        usernamePasswordArea.SetActive(false);
    }
    
    public void RegisterUserName()
    {
        if(userNameText1.text == string.Empty)  
            return;
        if(passwordText1.text == string.Empty)  
            return;
        RegisterWithUsername(userNameText1.text, passwordText1.text);
    }

    public void RegisterEmail()
    {
        if(emailText1.text == string.Empty)  
            return;
        if(emailPassText1.text == string.Empty)  
            return;
        RegisterWithEmail(emailText1.text, emailPassText1.text);
    }
    
    public void RegisterEmailUserName()
    {
        if(userNameText.text == string.Empty)  
            return;
        if(emailText.text == string.Empty)  
            return;
        if(emailPassText.text == string.Empty)  
            return;
        RegisterEmailAndUsername(emailText1.text, userNameText.text, emailPassText1.text);
    }


    private void RegisterWithEmail(string email, string password)
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email,
            Password = password,
            RequireBothUsernameAndEmail = false 
        };

        PlayFabClientAPI.RegisterPlayFabUser(request,
            result =>
            {
                Debug.Log("Signup Email successfully!");
            },
            error =>
            {
                Debug.LogError("Error signup Email: " + error.GenerateErrorReport());
            });
    }

    private void RegisterWithUsername(string username, string password)
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = username,
            Password = password,
            RequireBothUsernameAndEmail = false // không dùng email
        };

        PlayFabClientAPI.RegisterPlayFabUser(request,
            result =>
            {
                Debug.Log("Signup Email successfully!");
            },
            error =>
            {
                Debug.LogError("Error signup Email: " + error.GenerateErrorReport());
            });
    }

    private void RegisterEmailAndUsername(string email, string username, string password)
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email,
            Username = username,
            Password = password,
            RequireBothUsernameAndEmail = true
        };

        PlayFabClientAPI.RegisterPlayFabUser(request,
            result =>
            {
                Debug.Log("Signup Email + Username successfully");
            },
            error =>
            {
                Debug.LogError("Error signup: " + error.GenerateErrorReport());
            });
    }
}
