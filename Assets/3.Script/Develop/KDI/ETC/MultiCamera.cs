using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCamera : MonoBehaviour
{
    public static MultiCamera instance = null;

    public Animator loadingAnim;
    public Camera[] cameras;
    public int currentCamera = 0;
    public GameObject[] Dioramas;

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
        loading[0].SetActive(false);
        loading[1].SetActive(false);
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
                if (n == 1)
                {
                    Dioramas[0].SetActive(true);
                    Dioramas[1].SetActive(false);
                }
                else if (n == 2)
                {
                    Dioramas[0].SetActive(false);
                    Dioramas[1].SetActive(true);
                }
                cameras[i].gameObject.SetActive(true);
            }
        }
    }

    public void ToBattle()
    {
        loading[0].SetActive(true);
        loading[1].SetActive(true);
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
        loadingAnim.SetTrigger("active");
        FindObjectOfType<BattleLoader>().FieldBattle();
        Invoke("SetActiveFalse", 1f);

    }
    private void SetActiveFalse()
    {
        loading[0].SetActive(false);
        loading[1].SetActive(false);
    }
}
