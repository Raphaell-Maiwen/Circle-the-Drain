using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractMessenger : ScriptableObject
{
    public UnityEvent OnInteractPressed;

    public void SendInteractMessage(InputAction.CallbackContext context)
    {
        OnInteractPressed?.Invoke();
    }

    public void AddListener(UnityAction action)
    {
        OnInteractPressed.RemoveListener(action);
        OnInteractPressed.AddListener(action);
    }
}
