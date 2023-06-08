using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    AudioSource audioSource;
    public AudioClip audioButton;
    public AudioClip audioCloud;
    public AudioClip audioTurn;
    private void Awake()
    {
        instance = this;
        TryGetComponent(out audioSource);
    }

    public void PlayBtn()
    {
        audioSource.clip = audioButton;
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void PlayCloud()
    {
        audioSource.clip = audioCloud;
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void PlayTurn()
    {
        audioSource.clip = audioTurn;
        audioSource.PlayOneShot(audioSource.clip);
    }
}
