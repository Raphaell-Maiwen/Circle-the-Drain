using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

public class RocketLauncherTriggerZone : InteractableTriggerZone
{
    [SerializeField] private CandyLevelProgress _progress;
    [SerializeField] private CinemachineCamera _fireworkCamera;
    [SerializeField] private Fireworks _fireworks;

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
            _progress.LevelDone();

            CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Cutscene");

            _fireworkCamera.Priority = 10;
            AudioManager.Instance.StopAllSounds();
            _fireworks.InitializeFireworks();
        }
    }
}
