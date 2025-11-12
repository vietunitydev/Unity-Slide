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

   private void Start()
   {
      UpdateUI();
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
}
