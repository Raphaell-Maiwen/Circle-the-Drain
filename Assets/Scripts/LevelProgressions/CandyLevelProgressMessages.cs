using TMPro;
using UnityEngine;

public class CandyLevelProgressMessages : MonoBehaviour
{
    [SerializeField] private CandyLevelProgress _progress;
    [SerializeField] private InteractableTriggerZoneEventChannel _rocketLaunchZoneChannel;
    [SerializeField] private TextMeshProUGUI _teleportMessage;
    [SerializeField] private TextMeshProUGUI _rocketLaunchMessage;
    //Add the messages for the rockets

    private void OnEnable()
    {
        _progress.OnThresholdReached += ShowTeleportMessage;
        _rocketLaunchZoneChannel.OnPlayerEntered += ShowLaunchMessage;
    }

    private void OnDisable()
    {
        _progress.OnThresholdReached -= ShowTeleportMessage;
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

    private void EraseAllMessages()
    {
        _teleportMessage.gameObject.SetActive(false);
        _rocketLaunchMessage.gameObject.SetActive(false);
    }
}
