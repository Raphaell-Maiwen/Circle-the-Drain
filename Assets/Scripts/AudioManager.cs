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
        
        Debug.LogError("Sound name doesn't exist");
        return false; ;
    }

    //Add channels and stuff
    public void AddAudioDistortionFilter(float distortionLevel)
    {
        AudioDistortionFilter newFilter = gameObject.AddComponent<AudioDistortionFilter>();
        newFilter.distortionLevel = distortionLevel;
    }

    //Add depth?
    public void AddChorusFilter(float delay, float rate)
    {
        AudioChorusFilter newFilter = gameObject.AddComponent<AudioChorusFilter>();
        newFilter.delay = delay;
        newFilter.rate = rate;
        newFilter.depth = 1f;
    }

    public void AddReverbFilter(AudioReverbPreset preset)
    {
        AudioReverbFilter newFilter = gameObject.AddComponent<AudioReverbFilter>();
        newFilter.reverbPreset = preset; 
    }

    public void RemoveReverbFilter()
    {
        gameObject.TryGetComponent<AudioReverbFilter>(out AudioReverbFilter reverbFilter);

        if (reverbFilter)
        {
            DestroyImmediate(reverbFilter);
        }
    }

    public void SetDistortionFilter(float distortionLevel, bool increment = false)
    {
        gameObject.TryGetComponent<AudioDistortionFilter>(out AudioDistortionFilter distortionFilter);
        if (!distortionFilter)
        {
            AddAudioDistortionFilter(distortionLevel);
        }
        else
        {
            if(!increment) distortionFilter.distortionLevel = distortionLevel;
            else distortionFilter.distortionLevel += distortionLevel;
        }
    }

    public void SetChorusFilterDelay(float delay, bool increment = false)
    {
        gameObject.TryGetComponent<AudioChorusFilter>(out AudioChorusFilter chorusFilter);

        if (!chorusFilter)
        {
            AddChorusFilter(delay, 1);
        }
        else
        {
            if (!increment) chorusFilter.delay = delay;
            else chorusFilter.delay += delay;
        }
    }
    
    public void SetChorusFilterRate(float rate, bool increment = false)
    {
        gameObject.TryGetComponent<AudioChorusFilter>(out AudioChorusFilter chorusFilter);

        if (!chorusFilter)
        {
            AddChorusFilter(1, rate);
        }
        else
        {
            if (!increment) chorusFilter.rate = rate;
            else chorusFilter.rate += rate;
        }
    }
}





























