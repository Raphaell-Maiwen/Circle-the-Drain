using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CandyLevelProgress", menuName = "Scriptable Objects/CandyLevelProgress")]
public class CandyLevelProgress : ScriptableObject
{
    public int Count { get; private set; }
    [SerializeField] private int _threshold = 3;
    public int Threshold => _threshold;

    [SerializeField] private int _rocketsTotal;
    public int RocketsRemaining => _rocketsTotal - Count;

    public event Action OnRocketCollected;
    public event Action OnThresholdReached;
    public event Action OnLevelDone;

    public bool IsThresholdReached => Count >= Threshold;
    public bool IsLevelDone;


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

    public void LevelDone()
    {
        IsLevelDone = true;
        OnLevelDone?.Invoke();
    }

    public void Reset()
    {
        Count = 0;
        IsLevelDone = false;
    }
}
