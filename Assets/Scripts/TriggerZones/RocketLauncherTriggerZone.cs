using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class RocketLauncherTriggerZone : InteractableTriggerZone
{
    [SerializeField] private CandyLevelProgress _progress;
    [SerializeField] private CinemachineCamera _fireworkCamera;
    
    //Firework script
    //[SerializeField] private

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
            GetComponent<BoxCollider>().enabled = false;
            OnPlayerExit();

            CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Cutscene");

            _fireworkCamera.Priority = 10;
        }
    }
}
