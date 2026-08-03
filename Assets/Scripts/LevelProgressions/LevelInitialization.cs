using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInitialization : MonoBehaviour
{
    [SerializeField] protected LevelInfo _levelInfo;

    protected void Start()
    {
        AudioManager.Instance.SetLevelSong(_levelInfo._levelSong, _levelInfo._onLoop);
        GameObject ui = Instantiate(_levelInfo._UIPrefab);
        SceneManager.MoveGameObjectToScene(ui, gameObject.scene); 
        PlayerSpawner.Instance.SpawnPlayer(_levelInfo._playerStartingAnchor, _levelInfo._cameraStartingAnchor, _levelInfo);
    }
}
