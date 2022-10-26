using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip selectSound;
    [SerializeField] private AudioClip commandSound;

    public void PlaySelectSound()
    {
        audioSource.PlayOneShot(selectSound);
    }
    public void PlayCommandSound()
    {
        audioSource.PlayOneShot(commandSound);
    }
}
