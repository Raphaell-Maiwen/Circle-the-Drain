using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableTriggerZoneEventChannel", menuName = "Scriptable Objects/InteractableTriggerZoneEventChannel")]
public class InteractableTriggerZoneEventChannel : ScriptableObject
{
    public string DefaultMessage;
    public Func<string> GetMessage;

    public bool IsTriggered { get; private set; }
    public event Action<InteractableTriggerZoneEventChannel> OnPlayerEntered;
    public event Action OnPlayerExited;

    public void PlayerEnter()
    {
        IsTriggered = true;
        OnPlayerEntered?.Invoke(this);
    }

    public void PlayerExit()
    {
        IsTriggered = false;
        OnPlayerExited?.Invoke();
    }

    public string ResolveMessage() => GetMessage != null ? GetMessage() : DefaultMessage;
}
