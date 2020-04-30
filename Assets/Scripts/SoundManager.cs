using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    private static List<AudioSource> _audioSourcesList = new List<AudioSource>();
    
    public static AudioSource PlaySound(AudioClip clip, bool randomPitch = false, float minPitch = 0.6f, float maxPitch = 0.9f, float manualPitch = 1f)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        _audioSourcesList.Add(audioSource);
        if (randomPitch)
            audioSource.pitch = Random.Range(minPitch, maxPitch);
        else
            audioSource.pitch = manualPitch;
        audioSource.PlayOneShot(clip, GameManager.Instance.volume);
        _audioSourcesList.Add(audioSource);
        GameObject.Destroy(soundGameObject, clip.length);
        return (audioSource);
    }

    public static AudioSource PlayMusic(AudioClip clip, bool loop = false, float volume = 1f)
    {
        GameObject soundGameObject = new GameObject("Music");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        _audioSourcesList.Add(audioSource);
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.Play();
        return (audioSource);
    }

    public static void UpdateVolume(float value)
    {
        foreach (AudioSource AS in _audioSourcesList)
        {
            if (AS != null)
                AS.volume = value;
        }
    }
}