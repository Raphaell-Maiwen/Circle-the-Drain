using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CandyLevelProgress", menuName = "Scriptable Objects/CandyLevelProgress")]
public class CandyLevelProgress : ScriptableObject
{
    public int Count { get; private set; }
    [SerializeField] private int _threshold = 3;
    public int Threshold => _threshold;

    public event Action OnRocketCollected;
    public event Action OnThresholdReached;

    public bool IsThresholdReached => Count >= Threshold;


    //Temporary solution; remove this once we have our Single Entry Point
    private void OnEnable()
    {
        Reset();
    }

    public void Add()
    {
        Count++;
        OnRocketCollected?.Invoke();
        if (Count == Threshold)
        {
            OnThresholdReached?.Invoke();
        }
    }

    public void Reset() => Count = 0; 
}
