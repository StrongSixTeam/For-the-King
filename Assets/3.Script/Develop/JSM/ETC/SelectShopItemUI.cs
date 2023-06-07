using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectShopItemUI : MonoBehaviour
{
    [SerializeField] ShopManager shopManager;
    [SerializeField] Button btn;

    private void Awake()
    {
        shopManager = FindObjectOfType<ShopManager>();
    }
    void Start()
    {
        btn.onClick.AddListener(shopManager.ShowShopUI);
    }
}
