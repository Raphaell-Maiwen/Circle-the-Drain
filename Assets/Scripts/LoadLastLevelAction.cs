using System;
using UnityEngine;

public class LoadLastLevelAction : MonoBehaviour
{
    [SerializeField] private PrideParadeProgress _prideParadeProgress;
    [SerializeField] private InteractMessenger _interactMessenger;
    [SerializeField] private InteractableTriggerZoneEventChannel[] _zoneChannels;

    private void OnEnable()
    {
        _prideParadeProgress.OnEndReached += EnableLoadLastLevel;
        
        if(_prideParadeProgress.IsEndReached) EnableLoadLastLevel();

        foreach (var zone in _zoneChannels)
        {
            zone.OnPlayerEntered += DisableLoadLastLevel;
            zone.OnPlayerExited += TryRestoreLoadLastLevel;
        }
    }

    private void OnDisable()
    {
        _prideParadeProgress.OnEndReached -= EnableLoadLastLevel;

        foreach (var zone in _zoneChannels)
        {
            zone.OnPlayerEntered -= DisableLoadLastLevel;
            zone.OnPlayerExited -= TryRestoreLoadLastLevel;
        }

        _interactMessenger.OnInteractInput -= LoadLastLevel;
    }

    private void EnableLoadLastLevel() => _interactMessenger.OnInteractInput += LoadLastLevel;
    private void DisableLoadLastLevel(InteractableTriggerZoneEventChannel channel) => _interactMessenger.OnInteractInput -= LoadLastLevel;

    private void TryRestoreLoadLastLevel()
    {
        _interactMessenger.OnInteractInput -= LoadLastLevel;
        if(_prideParadeProgress.IsEndReached) EnableLoadLastLevel();
    }

    private void LoadLastLevel()
    {
        Debug.Log("LoadLastLevel");
    }
}
