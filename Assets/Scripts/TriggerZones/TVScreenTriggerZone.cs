using UnityEngine;
using WorkingTelevisions;

public class TVScreenTriggerZone : InteractableTriggerZone
{
    [SerializeField] private televisionCode TelevisionCode;

    protected override void OnInteractPressed(string str)
    {
        _interactMessenger.OnInteractPressed?.Invoke(null);
        GetComponent<BoxCollider>().enabled = false;
        OnPlayerExit();
        TelevisionCode.channelChange();
    }
}
