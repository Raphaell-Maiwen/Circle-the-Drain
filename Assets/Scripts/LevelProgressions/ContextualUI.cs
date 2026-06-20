using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ContextualUI : MonoBehaviour
{
    [SerializeField] private InteractableTriggerZoneEventChannel[] _zoneChannels;
    [SerializeField] private TextMeshProUGUI _zoneMessage;
    protected UnityEvent _disableExtraUI = new UnityEvent();
    protected UnityEvent _restoreState = new UnityEvent();

    protected InteractableTriggerZoneEventChannel _lastChannel;

    protected void OnEnable()
    {
        foreach (var channel in _zoneChannels)
        {
            channel.OnPlayerEntered += ShowZoneMessage;
            channel.OnPlayerExited += RestoreState;
            channel.OnUpdatedMessage += UpdateZoneMessage;
        }
    }

    protected void OnDisable()
    {
        foreach (var channel in _zoneChannels)
        {
            channel.OnPlayerEntered -= ShowZoneMessage;
            channel.OnPlayerExited -= RestoreState;
            channel.OnUpdatedMessage -= UpdateZoneMessage;
        }
    }

    protected void ShowZoneMessage(InteractableTriggerZoneEventChannel channel)
    {
        _disableExtraUI?.Invoke();
        _zoneMessage.text = channel.ResolveMessage();
        _zoneMessage.gameObject.SetActive(true);
        _lastChannel = channel;
    }

    public void UpdateZoneMessage(InteractableTriggerZoneEventChannel channel)
    {
        _zoneMessage.text = channel.ResolveMessage();
    }

    protected void EraseAllMessages()
    {
        _disableExtraUI?.Invoke();
        _zoneMessage.gameObject.SetActive(false);
    }

    private void RestoreState()
    {
        EraseAllMessages();
        _restoreState?.Invoke();
    }
}
