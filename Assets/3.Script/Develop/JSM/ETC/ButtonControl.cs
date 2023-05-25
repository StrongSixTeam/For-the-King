using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] Sprite defaultImage;
    [SerializeField] Sprite hoverImage;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    public void OnOver()
    {
        btn.transform.GetChild(0).GetComponentInChildren<Image>().sprite = hoverImage;
        //Debug.Log("Hover");
    }

    public void OnExit()
    {
        btn.transform.GetChild(0).GetComponentInChildren<Image>().sprite = defaultImage;
        //Debug.Log("Exit");
    }


}
