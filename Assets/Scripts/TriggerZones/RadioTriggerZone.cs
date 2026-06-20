using System;
using UnityEngine;

public class RadioTriggerZone : InteractableTriggerZone
{
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        _zoneChannel.GetMessage = () => _audioSource.isPlaying
            ? "Right-click to turn off radio."
            : "Right-click to turn on radio.";
    }

    protected override void OnInteractPressed(string str)
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Pause();
        }
        
        _zoneChannel.UpdateMessage();
    }
}
