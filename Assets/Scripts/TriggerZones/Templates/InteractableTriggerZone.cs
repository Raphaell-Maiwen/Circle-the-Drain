using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableTriggerZone : TriggerZone
{
    [SerializeField] private InteractMessenger _interactMessenger;
    [SerializeField] protected InteractableTriggerZoneEventChannel _zoneChannel;

    protected override void OnPlayerEnter()
    {
        _interactMessenger.AddListener(OnInteractPressed);
        _zoneChannel.PlayerEnter();
    }

    protected override void OnPlayerExit()
    {
        _interactMessenger.OnInteractPressed.RemoveListener(OnInteractPressed);
        _zoneChannel.PlayerExit();
    }

    protected abstract void OnInteractPressed(string str);
}
