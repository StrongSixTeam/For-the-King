using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    public Sound[] BGM = null;
    public AudioSource BGMPlayer;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        BGMPlayer = transform.GetComponent<AudioSource>();
    }

    private void Start()
    {
        BGMPlayer.clip = BGM[0].clip;
        BGMPlayer.Play();
    }
}
