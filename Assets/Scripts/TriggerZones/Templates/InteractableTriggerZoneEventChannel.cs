using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableTriggerZoneEventChannel", menuName = "Scriptable Objects/InteractableTriggerZoneEventChannel")]
public class InteractableTriggerZoneEventChannel : ScriptableObject
{
    public bool IsTriggered { get; private set; }
    public event Action OnPlayerEntered;
    public event Action OnPlayerExited;

    public void PlayerEnter()
    {
        IsTriggered = true;
        OnPlayerEntered?.Invoke();
    }

    public void PlayerExit()
    {
        IsTriggered = false;
        OnPlayerExited?.Invoke();
    }
}
