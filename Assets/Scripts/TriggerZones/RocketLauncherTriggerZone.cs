using TMPro;
using UnityEngine;

public class RocketLauncherTriggerZone : InteractableTriggerZone
{
    [SerializeField] private CandyLevelProgress _progress;
    [SerializeField] private InteractableTriggerZoneEventChannel _zoneChannel;

    private void Awake()
    {
        _zoneChannel.GetMessage = () => _progress.IsThresholdReached
            ? "Right-click to launch rockets"
            : $"You need {_progress.Threshold - _progress.Count} more rockets.";
    }

    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        _zoneChannel.PlayerEnter();
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();
        _zoneChannel.PlayerExit();
    }

    protected override void OnInteractPressed()
    {
        if (_progress.IsThresholdReached)
        {
            Debug.Log("Youpi!");
        }
        else
        {
            Debug.Log("Non.");
        }
    }
}
