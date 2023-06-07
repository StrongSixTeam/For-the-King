using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryBtn1 : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private ShopManager shopManager;
    public void InventoryOnOff()
    {
        shopManager = FindObjectOfType<ShopManager>();
        if (!inventory.activeSelf)
        {
            inventory.SetActive(true);
            GameObject Click = EventSystem.current.currentSelectedGameObject;
            inventory.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text =
                Click.transform.parent.parent.GetChild(0).GetChild(0).GetComponent<Text>().text;
            if (Click.transform.tag.Equals("Player0"))
            {
                InventoryController1.instance.playerNum = PlayerNum.Player0;
            }

            else if (Click.transform.tag.Equals("Player1"))
            {
                InventoryController1.instance.playerNum = PlayerNum.Player1;
            }

            else if (Click.transform.tag.Equals("Player2"))
            {
                InventoryController1.instance.playerNum = PlayerNum.Player2;
            }
            InventoryController1.instance.InventoryShow((int)InventoryController1.instance.playerNum);
            InventoryController1.instance.ShowEquip();
            if(shopManager != null)
            {
                shopManager.ShopSetting();
            }
        }
        else
        {
            inventory.SetActive(false);
            //InventoryController1.instance.playerNum = (PlayerNum)System.Enum.Parse(typeof(PlayerNum), GameManager.instance.nextTurn.ToString());
        }
    }
    public void BattleInventory()
    {
        shopManager = FindObjectOfType<ShopManager>();
        if (!inventory.activeSelf)
        {
            inventory.SetActive(true);
            GameObject Click = EventSystem.current.currentSelectedGameObject;
            if((GameManager.instance.nextTurn - 1) < 0)
            {
                inventory.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text =
                GameManager.instance.Players[2].GetComponent<PlayerStat>().name;
            }
            else
            {
                inventory.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text =
                GameManager.instance.Players[GameManager.instance.nextTurn - 1].GetComponent<PlayerStat>().name;
            }
            
            if (FindObjectOfType<BattleOrderManager>().Order[FindObjectOfType<BattleOrderManager>().turn].transform.tag.Equals("Player0"))
            {
                InventoryController1.instance.playerNum = PlayerNum.Player0;
            }

            else if (FindObjectOfType<BattleOrderManager>().Order[FindObjectOfType<BattleOrderManager>().turn].transform.tag.Equals("Player1"))
            {
                InventoryController1.instance.playerNum = PlayerNum.Player1;
            }

            else if (FindObjectOfType<BattleOrderManager>().Order[FindObjectOfType<BattleOrderManager>().turn].transform.tag.Equals("Player2"))
            {
                InventoryController1.instance.playerNum = PlayerNum.Player2;
            }
            InventoryController1.instance.InventoryShow((int)InventoryController1.instance.playerNum);
            InventoryController1.instance.ShowEquip();
            if (shopManager != null)
            {
                shopManager.ShopSetting();
            }
        }
        else
        {
            inventory.SetActive(false);
            //InventoryController1.instance.playerNum = (PlayerNum)System.Enum.Parse(typeof(PlayerNum), GameManager.instance.nextTurn.ToString());
        }
    }
}
