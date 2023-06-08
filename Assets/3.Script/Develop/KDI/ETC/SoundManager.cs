using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip audioButton;
    private void Awake()
    {
        TryGetComponent(out audioSource);
    }

    public void PlayBtn()
    {
        audioSource.clip = audioButton;
        audioSource.PlayOneShot(audioSource.clip);
    }
}
