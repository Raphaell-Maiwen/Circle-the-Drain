using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractMessenger : ScriptableObject
{
    public event Action OnInteractInput;
    public UnityEvent<string> OnInteractPressed;

    public void SendInteractMessage(InputAction.CallbackContext context)
    {
        OnInteractInput?.Invoke();
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
