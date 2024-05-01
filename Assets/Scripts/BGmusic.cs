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

    public void PopSound()
    {
        audioSource.PlayOneShot(pop);
    }

    public void MusicVolume(float musicVolume)
    {
        audioSource.volume = musicVolume;
    }
}
