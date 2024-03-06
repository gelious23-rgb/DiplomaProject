using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public audioStorage AudioStorage;
    public static audioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void playSound(audioStorage.audioType audioType)
    {
        AudioSource.PlayOneShot(AudioStorage.Get(audioType));
    }
}
