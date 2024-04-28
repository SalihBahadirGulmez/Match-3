using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGmusic : MonoBehaviour
{
    public GameObject dontDestroy;

    public AudioSource audioSource;
    public AudioClip pop;

    void Start()
    {
        DontDestroyOnLoad(dontDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PopSound()
    {
        audioSource.PlayOneShot(pop);
    }

    public void MusicVolume(float musicVolume)
    {
        audioSource.volume = musicVolume;
    }
}
