using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBtn1 : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    public void InventoryOnOff()
    {
        if (!inventory.activeSelf)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }
}
