using UnityEngine;

public class LevelInitialization : MonoBehaviour
{
    [SerializeField] protected LevelInfo _levelInfo;

    protected void Start()
    {
        AudioManager.Instance.SetLevelSong(_levelInfo._levelSong, _levelInfo._onLoop);
        Instantiate(_levelInfo._UIPrefab);
        PlayerSpawner.Instance.SpawnPlayer(_levelInfo._playerStartingAnchor, _levelInfo._cameraStartingAnchor, _levelInfo._levelIndex);
    }
}
