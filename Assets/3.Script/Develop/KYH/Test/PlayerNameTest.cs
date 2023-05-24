using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameTest : MonoBehaviour
{
    public void PlayerNameDebug()
    {
        Debug.Log(PlayerPrefs.GetString("Name0"));
        Debug.Log(PlayerPrefs.GetString("Name1"));
        Debug.Log(PlayerPrefs.GetString("Name2"));

        Debug.Log(PlayerPrefs.GetString("Class0"));
        Debug.Log(PlayerPrefs.GetString("Class1"));
        Debug.Log(PlayerPrefs.GetString("Class2"));

        Debug.Log(PlayerPrefs.GetInt("PlayerCnt"));
    }
}
