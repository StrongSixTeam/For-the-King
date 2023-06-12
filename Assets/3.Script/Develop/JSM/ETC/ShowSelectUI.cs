using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowSelectUI : MonoBehaviour
{
    [SerializeField] Button btn;
    Vector3 point;
    void Awake()
    {
        btn.onClick.AddListener(SetSelectUI);
        btn.onClick.AddListener(InventoryController1.instance.SelectInvenEquipItem);
    }

    public void SetSelectUI()
    {
        GameObject Click = EventSystem.current.currentSelectedGameObject;
        ItemType itemtype = (ItemType)System.Enum.Parse(typeof(ItemType), Click.transform.tag);
        point = Input.mousePosition;
        InventoryController1.instance.itemSelectUI.transform.position = new Vector3(point.x + 95f, point.y - 75, 0f);

        switch (itemtype)
        {
            case ItemType.����:
            case ItemType.��:
                InventoryController1.instance.isEquip = true;
                InventoryController1.instance.isUsed = false;
                // ���� ��ư Ȱ��ȭ
                InventoryController1.instance.itemSelectUI.transform.GetChild(1).GetComponentInChildren<Button>().interactable = true;
                InventoryController1.instance.itemSelectUI.transform.GetChild(1).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.actImage;
                InventoryController1.instance.itemSelectUI.transform.GetChild(1).GetChild(1).GetComponentInChildren<Text>().color = Color.white;


                // ����, �ݱ� �̿� �ٸ� ��ư ��Ȱ��ȭ
                InventoryController1.instance.itemSelectUI.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.deactImage;

                InventoryController1.instance.itemSelectUI.transform.GetChild(0).GetChild(1).GetComponentInChildren<Text>().color = Color.gray;

                InventoryController1.instance.itemSelectUI.transform.GetChild(0).GetComponentInChildren<Button>().interactable = false;
                break;
            case ItemType.��ũ��:
            case ItemType.���:
                InventoryController1.instance.isUsed = true;
                InventoryController1.instance.isEquip = false;
                // ��� ��ư
                InventoryController1.instance.itemSelectUI.transform.GetChild(0).GetComponentInChildren<Button>().interactable = true;
                InventoryController1.instance.itemSelectUI.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.actImage;
                InventoryController1.instance.itemSelectUI.transform.GetChild(0).GetChild(1).GetComponentInChildren<Text>().color = Color.white;

                // ���, �ݱ� �̿� �ٸ� ��ư ��Ȱ��ȭ
                InventoryController1.instance.itemSelectUI.transform.GetChild(1).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.deactImage;

                InventoryController1.instance.itemSelectUI.transform.GetChild(1).GetChild(1).GetComponentInChildren<Text>().color = Color.gray;

                InventoryController1.instance.itemSelectUI.transform.GetChild(1).GetComponentInChildren<Button>().interactable = false;
                break;
        }

        InventoryController1.instance.ShowItemSelectUI();
    }

    
}
