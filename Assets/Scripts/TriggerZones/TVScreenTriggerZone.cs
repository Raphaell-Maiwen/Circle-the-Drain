using UnityEngine;
using WorkingTelevisions;

public class TVScreenTriggerZeon : InteractableTriggerZone
{
    [SerializeField] private televisionCode TelevisionCode;

    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();
    }

    protected override void OnInteractPressed()
    {
        TelevisionCode.channelChange();
    }
}
