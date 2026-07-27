using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PrideParadeProgress", menuName = "Scriptable Objects/PrideParadeProgress")]
public class PrideParadeProgress : ScriptableObject
{
    public bool IsEndReached { get; private set; }
    
    public event Action OnEndReached;
    public event Action OnLevelDone;

    private void OnEnable()
    {
        Reset();
    }

    public void EndReached()
    {
        IsEndReached = true;
        OnEndReached?.Invoke();
    }

    public void LevelDone()
    {
        OnLevelDone?.Invoke();
    }

    public void Reset() => IsEndReached = false;
}
