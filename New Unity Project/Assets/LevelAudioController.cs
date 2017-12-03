using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LevelAudioController : MonoBehaviour {

    public static LevelAudioController Instance;

    AudioSource _audio;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("WTF");
        Instance = this;
        _audio = GetComponent<AudioSource>();
    }
    
    public void PlayOneShoot(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }

}
