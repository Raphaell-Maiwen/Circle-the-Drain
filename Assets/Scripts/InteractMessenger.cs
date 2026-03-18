using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractMessenger : ScriptableObject
{
    public UnityEvent<string> OnInteractPressed;

    public void SendInteractMessage(InputAction.CallbackContext context)
    {
        OnInteractPressed?.Invoke(null);
    }

    public void AddListener(UnityAction<string> action)
    {
        OnInteractPressed.RemoveListener(action);
        OnInteractPressed.AddListener(action);
    }

    public void RemoveListener(UnityAction<string> action)
    {
        OnInteractPressed.RemoveListener(action);
    }
}
