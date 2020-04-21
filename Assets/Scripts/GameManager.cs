using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool playBackgroundMusicClip;
    [SerializeField] private AudioClip backgroundMusicClip;
    [SerializeField] private bool playAmbientClip;
    [SerializeField] private AudioClip ambientClip;

    public static GameManager Instance;
    
    private float _prevVolume;
    [Range(0f, 1f)]
    public float volume;

    private AudioSource _backgroundAudioSource;
    private AudioSource _ambientAudioSource;
    
    void Awake() {
        Instance = this;
    }

    void Start()
    {
        _prevVolume = volume;
        if (playBackgroundMusicClip)
            _backgroundAudioSource = SoundManager.PlayMusic(backgroundMusicClip, true, volume);
        if (playAmbientClip)
            _ambientAudioSource = SoundManager.PlayMusic(ambientClip, true, volume);
    }

    private void Update()
    {
        if (volume != _prevVolume)
        {
            volume = Mathf.Clamp(volume, 0f, 1f);
            UpdateVolume(volume);
        }
        _prevVolume = volume;
    }
    
    private void UpdateVolume(float newVolume)
    {
        if (_backgroundAudioSource != null)
            _backgroundAudioSource.volume = newVolume;
        if (_ambientAudioSource != null)
            _ambientAudioSource.volume = newVolume;
        SoundManager.UpdateVolume(newVolume);
    }
}
