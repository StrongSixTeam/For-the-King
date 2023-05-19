using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IventroyController : MonoBehaviour
{
    [Header("아이템 리스트 프리팹")]
    [SerializeField] private GameObject itemlistPrebs;

    private Vector3 poolPos;
    private Vector2 ListPos; //프리팹 생성 위치

    private List<GameObject> poolItemList = new List<GameObject>();

    public List<Image> InvenImg = new List<Image>();
    public List<Text> InvenTxt = new List<Text>();

    private GameObject item;

    private int itemlistCnt = 0;

    private void Start()
    {
        poolPos = new Vector3(0, 0, -100);
        ListPos = new Vector3(0, 105);
    }
    private void Update()
    {
        if(itemlistCnt < QuickSlotController.instance.ItemList.Count)
        {
            ItemListStack();
            itemlistCnt++;
        }
        InventroyShow();
    }
    private void ItemListStack()
    {     
        if (poolItemList.Count != 0)
        {
            item = poolItemList[0];
            poolItemList.RemoveAt(0);
        }
        else
        {
            item = Instantiate(itemlistPrebs, poolPos, Quaternion.identity);
        }

        item.transform.SetParent(this.transform);
        item.transform.localPosition = ListPos;

        InvenImg.Add(item.GetComponentInChildren<Image>());
        InvenTxt.Add(item.GetComponentInChildren<Text>());

        ListPos = new Vector2(0, ListPos.y - 25);
    }
    public void InventroyShow()
    {
        for(int i = 0; i < InvenImg.Count; i++)
        {
            if (QuickSlotController.instance.ItemList[i].Equals("Herb"))
            {
                InvenImg[i].sprite = QuickSlotController.instance.herb;
                InvenTxt[i].text = "신의 수염 " + QuickSlotController.instance.herbCnt;
            }
            else if (QuickSlotController.instance.ItemList[i].Equals("Danceherb"))
            {
                InvenImg[i].sprite = QuickSlotController.instance.danceherb;
                InvenTxt[i].text = "춤추는 쐐기풀 " + QuickSlotController.instance.danceherbCnt;
            }
        }
    }
    private void ItemListUse()
    {
        
    }
}
