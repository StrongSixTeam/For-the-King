using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCamera : MonoBehaviour
{
    public Animator loadingAnim;
    public Animator loadingAnim2;
    public Camera[] cameras;
    int currentCam = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            loadingAnim.SetTrigger("cloud");
            loadingAnim2.SetTrigger("cloud");
            Invoke("CloudOn", 0.7f);
            Invoke("CloudOff", 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            loadingAnim.SetTrigger("cloud");
            loadingAnim2.SetTrigger("cloud");
            Invoke("CloudOn2", 0.7f);
            Invoke("CloudOff", 1.5f);
        }

    }

    private void ChangeCam(int n)
    {
        if (n == 0)
        {
            cameras[1].enabled = false;
            cameras[0].enabled = true;
        }
        else
        {
            cameras[1].enabled = true;
            cameras[0].enabled = false;
        }
    }

    private void CloudOn()
    {
        currentCam = 1;
        ChangeCam(1);
    }

    private void CloudOn2()
    {
        currentCam = 0;
        ChangeCam(0);
    }

    private void CloudOff()
    {
        loadingAnim.SetTrigger("off");
        loadingAnim2.SetTrigger("off");
    }

    public void poi()
    {

    }
}
