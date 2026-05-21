using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InDialogEventChannel", menuName = "Scriptable Objects/InDialogEventChannel")]
public class InDialogEventChannel : ScriptableObject
{
    public event Action OnStartDialog;
    public event Action OnEndDialog;
    
    public void StartDialog()
    {
        OnStartDialog?.Invoke();
    }
    
    public void EndDialog()
    {
        OnEndDialog?.Invoke();
    }
}
