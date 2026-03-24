using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauntedHouseInitializer: LevelInitialization
{
    [SerializeField] private float _playerShrinkedSize;

    void Start()
    {
        base.Start();
        
        PlayerSpawner.Instance.ChangePlayerSize(_playerShrinkedSize);
        PlayerSpawner.Instance.ChangePlayerRotation(_levelInfo._playerStartingAnchor, _levelInfo._cameraStartingAnchor);
    }
}