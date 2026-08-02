using System;
using UnityEngine;

public class RadioTriggerZone : InteractableTriggerZone
{
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        _zoneChannel.GetMessage = () => _audioSource.isPlaying
            ? "Press A to turn off radio."
            : "Press A to turn on radio.";
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
