using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCamera : MonoBehaviour
{
    public static MultiCamera instance = null;

    public Animator loadingAnim;
    public Camera[] cameras;
    public int currentCamera = 0;
    public GameObject Dioramas;

    [SerializeField] private GameObject[] loading;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cameras[0].gameObject.SetActive(true);
        cameras[1].gameObject.SetActive(false);
        cameras[2].gameObject.SetActive(false);
        //loading[0].SetActive(false);
        //loading[1].SetActive(false);
    }

    public void StartCloud()
    {
        Invoke("StartCloudOff", 1f);
    }
    public void StartCloudOff()
    {
        SoundManager.instance.PlayCloud();
        loadingAnim.SetTrigger("active");
        Invoke("SetActiveFalse", 0.9f);
    }


    public void ChangeCam(int n) //n = 0 main, n = 1 battle, n = 2 cave
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (i != n)
            {
                cameras[i].gameObject.SetActive(false);
            }
            else
            {
                if (n == 0)
                {
                    Dioramas.SetActive(false);
                }
                else if (n == 1)
                {
                    Dioramas.SetActive(true);
                }
                else if (n == 2)
                {
                    Dioramas.SetActive(true);
                }
                cameras[i].gameObject.SetActive(true);
            }
        }
    }

    public void ToBattle()
    {
        loading[0].SetActive(true);
        loading[1].SetActive(true);
        SoundManager.instance.PlayCloud();
        loadingAnim.SetTrigger("active");
        Invoke("Act1", 1f);
    }
    private void Act1()
    {
        ChangeCam(1);
        Invoke("OffCloud", 1f);

    }
    private void OffCloud()
    {
        SoundManager.instance.PlayCloud();
        loadingAnim.SetTrigger("active");
        FindObjectOfType<BattleLoader>().FieldBattle();
        Invoke("SetActiveFalse", 1f);

    }
    private void SetActiveFalse()
    {
        loading[0].SetActive(false);
        loading[1].SetActive(false);
    }
    public void ToCave()
    {
        loading[0].SetActive(true);
        loading[1].SetActive(true);
        SoundManager.instance.PlayCloud();
        loadingAnim.SetTrigger("active");
        Invoke("Act2", 1f);
    }

    private void Act2()
    {
        ChangeCam(2);
        Invoke("OffCloud2", 1f);
    }

    private void OffCloud2()
    {
        SoundManager.instance.PlayCloud();
        loadingAnim.SetTrigger("active");
        FindObjectOfType<BattleLoader>().CaveBattle();
        Invoke("SetActiveFalse", 1f);
    }



    public void ToMain()
    {
        loading[0].SetActive(true);
        loading[1].SetActive(true);
        SoundManager.instance.PlayCloud();
        loadingAnim.SetTrigger("active");
        Invoke("Act3", 1f);
    }
    private void Act3()
    {
        ChangeCam(0);
        EncounterManager.instance.OnMovingUIs();
        Invoke("OffCloud3", 1f);

    }
    private void OffCloud3()
    {
        SoundManager.instance.PlayCloud();
        loadingAnim.SetTrigger("active");
        Invoke("SetActiveFalse", 1f);
    }

}
