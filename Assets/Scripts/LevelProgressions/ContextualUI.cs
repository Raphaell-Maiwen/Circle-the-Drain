using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ContextualUI : MonoBehaviour
{
    [SerializeField] private InteractableTriggerZoneEventChannel[] _zoneChannels;
    [SerializeField] private SubtitlesEventChannel _subtitlesEventChannel;
    [SerializeField] private TextMeshProUGUI _zoneMessage;
    [SerializeField] protected TextMeshProUGUI _subtitlesText;
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

        if (_subtitlesEventChannel)
        {
            _subtitlesEventChannel.OnSubtitlesUpdated += ShowSubtitles;
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
        
        if (_subtitlesEventChannel)
        {
            _subtitlesEventChannel.OnSubtitlesUpdated -= ShowSubtitles;
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

    public void ShowSubtitles(string text)
    {
        _subtitlesText.text = text;
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
