using TMPro;
using UnityEngine;

public class RocketLauncherTriggerZone : InteractableTriggerZone
{
    [SerializeField] private CandyLevelProgress _progress;

    private void Awake()
    {
        _zoneChannel.GetMessage = () => _progress.IsThresholdReached
            ? "Right-click to launch rockets"
            : $"You need {_progress.Threshold - _progress.Count} more rockets.";
    }

    protected override void OnInteractPressed()
    {
        if (_progress.IsThresholdReached)
        {
            Debug.Log("Youpi!");
            GetComponent<BoxCollider>().enabled = false;
            OnPlayerExit();
        }
        else
        {
            Debug.Log("Non.");
        }
    }
}
