using UnityEngine;
using WorkingTelevisions;

public class TVScreenTriggerZone : InteractableTriggerZone
{
    [SerializeField] private televisionCode TelevisionCode;

    protected override void OnInteractPressed()
    {
        GetComponent<BoxCollider>().enabled = false;
        OnPlayerExit();
        TelevisionCode.channelChange();
    }
}
