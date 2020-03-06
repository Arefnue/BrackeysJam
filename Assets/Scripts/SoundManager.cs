using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    

    #endregion
    
    // Audio players components.
    public AudioSource effectsSource;
    public AudioSource musicSource;
    
    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        effectsSource.clip = clip;
        effectsSource.Play();
    }
    
    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
    
}
