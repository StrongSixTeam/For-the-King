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
        InventoryController1.instance.itemSelectUI.transform.position = new Vector3(point.x + 95f, point.y - 120, 0f);

        switch (itemtype)
        {
            case ItemType.무기:
            case ItemType.방어구:
                InventoryController1.instance.isEquip = true;
                InventoryController1.instance.isUsed = false;
                // 장착 버튼 활성화
                InventoryController1.instance.itemSelectUI.transform.GetChild(1).GetComponentInChildren<Button>().interactable = true;
                InventoryController1.instance.itemSelectUI.transform.GetChild(1).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.actImage;
                InventoryController1.instance.itemSelectUI.transform.GetChild(1).GetChild(1).GetComponentInChildren<Text>().text =
                    "<color=#FFFFFF>장착</color>";


                // 장착, 닫기 이외 다른 버튼 비활성화
                InventoryController1.instance.itemSelectUI.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.deactImage;
                InventoryController1.instance.itemSelectUI.transform.GetChild(2).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.deactImage;
                InventoryController1.instance.itemSelectUI.transform.GetChild(3).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.deactImage;

                InventoryController1.instance.itemSelectUI.transform.GetChild(0).GetChild(1).GetComponentInChildren<Text>().text =
                    "<color=#BDBDBD>사용</color>";
                InventoryController1.instance.itemSelectUI.transform.GetChild(2).GetChild(1).GetComponentInChildren<Text>().text =
                    "<color=#BDBDBD>판매</color>";
                InventoryController1.instance.itemSelectUI.transform.GetChild(3).GetChild(1).GetComponentInChildren<Text>().text =
                    "<color=#BDBDBD>주기</color>";

                InventoryController1.instance.itemSelectUI.transform.GetChild(0).GetComponentInChildren<Button>().interactable = false;
                InventoryController1.instance.itemSelectUI.transform.GetChild(2).GetComponentInChildren<Button>().interactable = false;
                InventoryController1.instance.itemSelectUI.transform.GetChild(3).GetComponentInChildren<Button>().interactable = false;
                break;
            case ItemType.스크롤:
            case ItemType.허브:
                InventoryController1.instance.isUsed = true;
                InventoryController1.instance.isEquip = false;
                // 사용 버튼
                InventoryController1.instance.itemSelectUI.transform.GetChild(0).GetComponentInChildren<Button>().interactable = true;
                InventoryController1.instance.itemSelectUI.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.actImage;
                InventoryController1.instance.itemSelectUI.transform.GetChild(0).GetChild(1).GetComponentInChildren<Text>().text =
                    "<color=#FFFFFF>사용</color>";

                // 사용, 닫기 이외 다른 버튼 비활성화
                InventoryController1.instance.itemSelectUI.transform.GetChild(1).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.deactImage;
                InventoryController1.instance.itemSelectUI.transform.GetChild(2).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.deactImage;
                InventoryController1.instance.itemSelectUI.transform.GetChild(3).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.deactImage;

                InventoryController1.instance.itemSelectUI.transform.GetChild(1).GetChild(1).GetComponentInChildren<Text>().text =
                    "<color=#BDBDBD>장착</color>";
                InventoryController1.instance.itemSelectUI.transform.GetChild(2).GetChild(1).GetComponentInChildren<Text>().text =
                    "<color=#BDBDBD>판매</color>";
                InventoryController1.instance.itemSelectUI.transform.GetChild(3).GetChild(1).GetComponentInChildren<Text>().text =
                    "<color=#BDBDBD>주기</color>";

                InventoryController1.instance.itemSelectUI.transform.GetChild(1).GetComponentInChildren<Button>().interactable = false;
                InventoryController1.instance.itemSelectUI.transform.GetChild(2).GetComponentInChildren<Button>().interactable = false;
                InventoryController1.instance.itemSelectUI.transform.GetChild(3).GetComponentInChildren<Button>().interactable = false;
                break;
        }

        InventoryController1.instance.ShowItemSelectUI();
    }

    
}
