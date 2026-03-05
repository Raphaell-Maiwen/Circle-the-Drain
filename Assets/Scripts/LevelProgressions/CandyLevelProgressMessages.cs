using TMPro;
using UnityEngine;

public class CandyLevelProgressMessages : MonoBehaviour
{
    [SerializeField] private CandyLevelProgress _progress;
    [SerializeField] private InteractableTriggerZoneEventChannel[] _zoneChannels;
    [SerializeField] private TextMeshProUGUI _teleportMessage;
    [SerializeField] private TextMeshProUGUI _zoneMessage;

    private void OnEnable()
    {
        _progress.OnThresholdReached += ShowTeleportMessage;

        foreach (var channel in _zoneChannels)
        {
            channel.OnPlayerEntered += ShowZoneMessage;
            channel.OnPlayerExited += RestoreState;
        }
    }

    private void OnDisable()
    {
        _progress.OnThresholdReached -= ShowTeleportMessage;

        foreach (var channel in _zoneChannels)
        {
            channel.OnPlayerEntered -= ShowZoneMessage;
            channel.OnPlayerExited -= RestoreState;
        }
    }

    private void ShowTeleportMessage()
    {
        EraseAllMessages();
        _teleportMessage.gameObject.SetActive(true);
    }

    private void ShowZoneMessage(InteractableTriggerZoneEventChannel channel)
    {
        _teleportMessage.gameObject.SetActive(false);
        _zoneMessage.text = channel.ResolveMessage();
        _zoneMessage.gameObject.SetActive(true);
    }

    private void EraseAllMessages()
    {
        _teleportMessage.gameObject.SetActive(false);
        _zoneMessage.gameObject.SetActive(false);
    }

    private void RestoreState()
    {
        EraseAllMessages();
        _teleportMessage.gameObject.SetActive(_progress.IsThresholdReached);
    }
}
