using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractMessenger : ScriptableObject
{
    public UnityEvent OnInteractPressed;

    public void SendInteractMessage(InputAction.CallbackContext context)
    {
        Debug.Log("Send message!");
        OnInteractPressed?.Invoke();
    }
}
