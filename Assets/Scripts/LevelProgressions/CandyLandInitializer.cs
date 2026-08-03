using UnityEngine;

public class CandyLandInitializer : LevelInitialization
{
    [SerializeField] private CandyLevelProgress _candyLevelProgress;

    void Start()
    {
        base.Start();
        
        _candyLevelProgress.Reset();
    }
}
