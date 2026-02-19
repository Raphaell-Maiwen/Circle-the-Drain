using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableTriggerZone : TriggerZone
{
    [SerializeField] private InteractMessenger _interactMessenger;

    protected override void OnPlayerEnter()
    {
        _interactMessenger.OnInteractPressed.AddListener(OnInteractPressed);
    }

    protected override void OnPlayerExit()
    {
        _interactMessenger.OnInteractPressed.RemoveListener(OnInteractPressed);
    }

    protected abstract void OnInteractPressed();
}
