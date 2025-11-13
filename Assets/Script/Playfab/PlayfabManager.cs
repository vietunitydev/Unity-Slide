using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayfabManager : MonoBehaviour
{
    [SerializeField] private GameObject loginArea;
    [SerializeField] private GameObject signupArea;
    [SerializeField] private GameObject playfabArea;

    private void Start()
    {
        DisableAll();
    }

    public void ShowLogin()
    {
        DisableAll();
        loginArea.SetActive(!loginArea.activeSelf);
    }
    public void ShowSignup()
    {
        DisableAll();
        signupArea.SetActive(!signupArea.activeSelf);
    }
    public void ShowPlayfab()
    {
        DisableAll();
        playfabArea.SetActive(!playfabArea.activeSelf);
    }

    private void DisableAll()
    {
        loginArea.SetActive(false);
        signupArea.SetActive(false);
        playfabArea.SetActive(false);
    }
}
