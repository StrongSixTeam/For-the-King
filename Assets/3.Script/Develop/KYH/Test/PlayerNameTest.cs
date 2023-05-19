using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameTest : MonoBehaviour
{
    public void PlayerNameDebug()
    {
        Debug.Log(PlayerPrefs.GetString("Player1"));
        Debug.Log(PlayerPrefs.GetString("Player2"));
        Debug.Log(PlayerPrefs.GetString("Player3"));
    }
}
