using UnityEngine;
using UnityEngine.Audio;
using AYellowpaper.SerializedCollections;
using NUnit.Framework.Constraints;

public class AudioManager : MonoBehaviour
{
    public SerializedDictionary<string, AudioClip> _bgm;
    public SerializedDictionary<string, AudioClip> _sfx;

    public SerializedDictionary<string, AudioSource> _gameSounds = new SerializedDictionary<string, AudioSource>();

    private void Awake()
    {
        foreach (var sound in _bgm)
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

    private void AddAudioComponent(Component audioComponent)
    {
        
    }

    public void PlaySound(string soundName)
    {
        if (_gameSounds.ContainsKey(soundName))
        {
            _gameSounds[soundName].Play();
        }
        else
        {
            Debug.LogError("Sound name doesn't exist");
        }
    }
}
