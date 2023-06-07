using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryChange : MonoBehaviour
{
    ShopManager shopManager;
    public void LeftChange()
    {
        if ((int)InventoryController1.instance.playerNum <= 0)
        {
            InventoryController1.instance.playerNum = (PlayerNum)2;
        }

        else
        {
            int num = (int)InventoryController1.instance.playerNum;
            InventoryController1.instance.playerNum = (PlayerNum)(--num);
        }
        transform.parent.GetChild(3).GetChild(0).GetComponent<Text>().text = GameManager.instance.playerStats[(int)InventoryController1.instance.playerNum].name;
        InventoryController1.instance.InventoryShow((int)InventoryController1.instance.playerNum);
        InventoryController1.instance.ShowEquip();
        shopManager = FindObjectOfType<ShopManager>();
        if (shopManager != null) shopManager.ShopSetting();
        
    }

    public void RightChange()
    {
        if ((int)InventoryController1.instance.playerNum >= 2)
        {
            InventoryController1.instance.playerNum = (PlayerNum)0;
        }

        else
        {
            int num = (int)InventoryController1.instance.playerNum;
            InventoryController1.instance.playerNum = (PlayerNum)(++num);
        }
        transform.parent.GetChild(3).GetChild(0).GetComponent<Text>().text = GameManager.instance.playerStats[(int)InventoryController1.instance.playerNum].name;
        InventoryController1.instance.InventoryShow((int)InventoryController1.instance.playerNum);
        InventoryController1.instance.ShowEquip();
        shopManager = FindObjectOfType<ShopManager>();
        if (shopManager != null) shopManager.ShopSetting();
    }
}
