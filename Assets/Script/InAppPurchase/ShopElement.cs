using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    [SerializeField] private IAPProductKey type;

    private void Start()
    {
        Button btn = GetComponentInChildren<Button>();
        if (btn == null) return;
        
        btn.onClick.AddListener(BuyItem);
    }

    private void BuyItem()
    {
        IAPManager.Instance.BuyProduct(type);
    }
}
