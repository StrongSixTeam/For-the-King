using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryBtn1 : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    public void InventoryOnOff()
    {
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
        }
        else
        {
            inventory.SetActive(false);
        }
    }
}
