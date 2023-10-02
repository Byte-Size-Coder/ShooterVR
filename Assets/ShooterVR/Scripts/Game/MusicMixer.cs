using UnityEngine;

public class MusicMixer : MonoBehaviour
{
    public AudioSource musicTrack;

    public AudioClip introClip;
    public AudioClip gameClip;
    public static MusicMixer Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            IntroTrack();
        }
    }

    public void IntroTrack()
    {
        musicTrack.clip = introClip;
        musicTrack.Play();
    }

    public void GameTrack()
    {
        musicTrack.clip = gameClip;
        musicTrack.Play();

    }
}
