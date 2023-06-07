using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassInfo : MonoBehaviour
{
    public ClassNameBtn classNameBtn;
    [SerializeField] private int num = 0;
    private bool show = false;
    [SerializeField] private GameObject classInfo;

    // Update is called once per frame
    private void Start()
    {
       
    }
    void Update()
    {
        num = classNameBtn.num;
        if (show)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == num)
                {
                    classInfo.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    classInfo.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    public void OnOffClassName()
    {
        if (!show)
        {
            classInfo.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                if (i == num)
                {
                    classInfo.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    classInfo.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            show = true;
        }
        else
        {
            classInfo.SetActive(false);
            show = false;
        }
    }
}
