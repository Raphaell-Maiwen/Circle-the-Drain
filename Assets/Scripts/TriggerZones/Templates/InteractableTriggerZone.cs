using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableTriggerZone : TriggerZone
{
    [SerializeField] protected InteractMessenger _interactMessenger;
    [SerializeField] protected InteractableTriggerZoneEventChannel _zoneChannel;

    protected override void OnPlayerEnter()
    {
        _interactMessenger.OnInteractInput += TriggerInteract;
        _zoneChannel.PlayerEnter();
    }

    protected override void OnPlayerExit()
    {
        _interactMessenger.OnInteractInput -= TriggerInteract;
        _zoneChannel.PlayerExit();
    }

    private void TriggerInteract()
    {
        OnInteractPressed(null);
    }

    protected abstract void OnInteractPressed(string str);
}
