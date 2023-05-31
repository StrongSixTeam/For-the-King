using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryBtn : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    public void InventoryOnOff()
    {
        if (!inventory.activeSelf)
        {
            GameObject Click = EventSystem.current.currentSelectedGameObject;
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

            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }
}
