using TMPro;
using UnityEngine;

public class CandyLevelProgressMessages : MonoBehaviour
{
    [SerializeField] private CandyLevelProgress _progress;
    [SerializeField] private InteractableTriggerZoneEventChannel _rocketLaunchZoneChannel;
    [SerializeField] private InteractableTriggerZoneEventChannel _rocketCollectableZoneChannel;
    [SerializeField] private TextMeshProUGUI _teleportMessage;
    [SerializeField] private TextMeshProUGUI _rocketLaunchMessage;
    [SerializeField] private TextMeshProUGUI _rocketCollectableMessage;

    private void OnEnable()
    {
        _progress.OnThresholdReached += ShowTeleportMessage;
        _rocketLaunchZoneChannel.OnPlayerEntered += ShowLaunchMessage;
        _rocketLaunchZoneChannel.OnPlayerExited += RestoreState;

        _rocketCollectableZoneChannel.OnPlayerEntered += ShowCollectMessage;
        _rocketCollectableZoneChannel.OnPlayerExited += RestoreState;
    }

    private void OnDisable()
    {
        _progress.OnThresholdReached -= ShowTeleportMessage;

        _rocketLaunchZoneChannel.OnPlayerEntered -= ShowLaunchMessage;
        _rocketLaunchZoneChannel.OnPlayerExited -= RestoreState;

        _rocketCollectableZoneChannel.OnPlayerEntered -= ShowCollectMessage;
        _rocketCollectableZoneChannel.OnPlayerExited -= RestoreState;
    }

    private void ShowTeleportMessage()
    {
        EraseAllMessages();
        _teleportMessage.gameObject.SetActive(true);
    }

    private void ShowLaunchMessage()
    {
        EraseAllMessages();
        int remaining = _progress.Threshold - _progress.Count;
        _rocketLaunchMessage.text = _progress.IsThresholdReached
            ? "Right-click to launch rockets"
            : $"You need {remaining} more rockets.";
        _rocketLaunchMessage.gameObject.SetActive(true);
    }

    private void ShowCollectMessage()
    {
        EraseAllMessages();
        _rocketCollectableMessage.gameObject.SetActive(true);
    }

    private void EraseAllMessages()
    {
        _teleportMessage.gameObject.SetActive(false);
        _rocketLaunchMessage.gameObject.SetActive(false);
        _rocketCollectableMessage.gameObject.SetActive(false);
    }

    private void RestoreState()
    {
        EraseAllMessages();
        _teleportMessage.gameObject.SetActive(_progress.IsThresholdReached);
    }
}
