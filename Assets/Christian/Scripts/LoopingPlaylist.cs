using UnityEngine;
using System.Collections.Generic;

public class LoopingPlaylist : MonoBehaviour
{
    public List<AudioClip> playlist;
    private AudioSource audioSource;
    private int currentSongIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (playlist.Count > 0)
        {
            PlayCurrentSong();
        }
        else
        {
            Debug.LogWarning("Spellistan är tom!");
        }
    }

    void Update()
    {
        if (!audioSource.isPlaying && playlist.Count > 0)
        {
            PlayNextSong();
        }
    }

    void PlayCurrentSong()
    {
        audioSource.clip = playlist[currentSongIndex];
        audioSource.Play();
        Debug.Log("Spelar: " + playlist[currentSongIndex].name);
    }

    void PlayNextSong()
    {
        currentSongIndex++;
        if (currentSongIndex >= playlist.Count)
        {
            currentSongIndex = 0; // Loopa tillbaka till början
        }
        PlayCurrentSong();
    }
}