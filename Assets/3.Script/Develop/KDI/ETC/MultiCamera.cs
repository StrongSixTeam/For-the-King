using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCamera : MonoBehaviour
{
    public static MultiCamera instance = null;

    public Animator loadingAnim;
    public Animator loadingAnim2;
    public Camera[] cameras;
    public bool check = false;

    [SerializeField] private GameObject[] clouds;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cameras[0].enabled = true;
        cameras[1].enabled = false;
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].SetActive(false);
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    loadingAnim.SetTrigger("cloud");
        //    loadingAnim2.SetTrigger("cloud");
        //    Invoke("CloudOn", 0.7f);
        //    Invoke("CloudOff", 1.5f);
        //}

        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    loadingAnim.SetTrigger("cloud");
        //    loadingAnim2.SetTrigger("cloud");
        //    Invoke("CloudOn2", 0.7f);
        //    Invoke("CloudOff", 1.5f);
        //}

    }

    public void ChangeCam()
    {
        if (check)
        {
            cameras[0].enabled = true;
            cameras[1].enabled = false;
            check = false;
        }
        else
        {
            cameras[0].enabled = false;
            cameras[1].enabled = true;
            check = true;
        }
    }

    public void MakeCloud()
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].SetActive(true);
        }
        ActiveTrigger();
        Invoke("ChangeCam", 1.5f);
        Invoke("ActiveTrigger", 2f);
        Invoke("DontShow",3f);

    }
    private void ActiveTrigger()
    {
        loadingAnim.SetTrigger("cloud");
        loadingAnim2.SetTrigger("cloud");
    }

    private void DontShow()
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].SetActive(false);
        }
    }
}
