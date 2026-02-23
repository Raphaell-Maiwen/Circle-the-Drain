using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public class ClassStaticPourProuverUnPointAErwan : MonoBehaviour
{
    public SerializedDictionary<string, AudioClip> _bgm;
    public SerializedDictionary<string, AudioClip> _sfx;

    private Dictionary<string, AudioSource> _gameSounds = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        foreach (var sound in _bgm)
        {
            AddSound(sound.Key, sound.Value);
        }

        foreach (var sound in _sfx)
        {
            AddSound(sound.Key, sound.Value);
        }
    }

    private void AddSound(string soundName, AudioClip audioClip)
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = audioClip;
        _gameSounds.Add(soundName, audioSource);
    }

    public void PlaySound(string soundName, bool onLoop)
    {
        if (_gameSounds.ContainsKey(soundName))
        {
            _gameSounds[soundName].Play();
            _gameSounds[soundName].loop = onLoop;
        }
        else
        {
            Debug.LogError("Sound name doesn't exist");
        }
    }

    public void StopSound(string soundName)
    {
        if (_gameSounds.ContainsKey(soundName))
        {
            _gameSounds[soundName].Stop();
        }
        else
        {
            Debug.LogError("Sound name doesn't exist");
        }
    }

    //Add channels and stuff
    public void AddFilter(AudioDistortionFilter filter, float distortionLevel)
    {
        gameObject.TryGetComponent<AudioDistortionFilter>(out AudioDistortionFilter distortionFilter);
        if (distortionFilter)
        {
            distortionFilter.distortionLevel += distortionLevel;
        }
        else
        {
            AudioDistortionFilter newFilter = gameObject.AddComponent<AudioDistortionFilter>();
            newFilter.distortionLevel = distortionLevel;
        }
    }
}
