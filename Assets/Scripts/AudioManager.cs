using UnityEngine;
using UnityEngine.Audio;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public SerializedDictionary<string, AudioClip> _bgm;
    public SerializedDictionary<string, AudioClip> _sfx;

    private Dictionary<string, AudioSource> _gameSounds = new Dictionary<string, AudioSource>();

    private string _levelSong;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (var sound in _bgm)
        {
            AddSound(sound.Key, sound.Value);
        }

        foreach (var sound in _sfx)
        {
            AddSound(sound.Key, sound.Value);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    private void AddSound(string soundName, AudioClip audioClip)
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = audioClip;
        _gameSounds.Add(soundName, audioSource);
    }

    public void SetLevelSong(string songName, bool onLoop = false)
    {
        _levelSong = songName;
        PlaySound(songName, onLoop);
    }

    public string GetLevelSong()
    {
        return _levelSong;
    }

    public void PlaySound(string soundName, bool onLoop = false)
    {
        if (SoundExist(soundName))
        {
            _gameSounds[soundName].Play();
            _gameSounds[soundName].loop = onLoop;
        }
    }

    public void StopSound(string soundName)
    {
        if(SoundExist(soundName)) { _gameSounds[soundName].Stop();}
    }

    public void StopAllSounds()
    {
        foreach (var sound in _gameSounds)
        {
            StopSound(sound.Key);
        }
    }

    public AudioSource GetAudioSource(string soundName)
    {
        return SoundExist(soundName) ? _gameSounds[soundName] : null;
    }

    private bool SoundExist(string soundName)
    {
        if (_gameSounds.ContainsKey(soundName)) return true;
        else
        {
            Debug.LogError("Sound name doesn't exist");
            return false; ;
        }
    }

    //Add channels and stuff
    public bool AddAudioDistortionFilter(float distortionLevel, float capDistortion = 0.9f)
    {
        gameObject.TryGetComponent<AudioDistortionFilter>(out AudioDistortionFilter distortionFilter);
        if (distortionFilter)
        {
            distortionFilter.distortionLevel += distortionLevel;
            distortionFilter.distortionLevel = Mathf.Min(distortionFilter.distortionLevel, capDistortion);
            if (distortionFilter.distortionLevel >= capDistortion) return true;
            return false;
        }
        else
        {
            AudioDistortionFilter newFilter = gameObject.AddComponent<AudioDistortionFilter>();
            newFilter.distortionLevel = distortionLevel;
            return false;
        }
    }

    //Add depth?
    public bool AddChorusFilter(float delay, float rate, float capDelay = 100f, float capRate = 20f)
    {
        gameObject.TryGetComponent<AudioChorusFilter>(out AudioChorusFilter chorusFilter);

        if (chorusFilter)
        {
            chorusFilter.delay += delay;
            chorusFilter.rate += rate;

            chorusFilter.delay = Mathf.Min(chorusFilter.delay, capDelay);
            chorusFilter.rate = Mathf.Min(chorusFilter.rate, capRate);

            if ((delay > 0 && chorusFilter.delay >= capDelay) ||
            (rate > 0 && chorusFilter.rate >= capRate)) return true;
            return false;
        }
        else
        {
            AudioChorusFilter newFilter = gameObject.AddComponent<AudioChorusFilter>();
            newFilter.delay = delay;
            newFilter.rate = rate;
            newFilter.depth = 1f;

            return false;
        }
    }
}
