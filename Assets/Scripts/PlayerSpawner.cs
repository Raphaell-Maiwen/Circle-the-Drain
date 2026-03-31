using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance { get; private set; }

    [SerializeField] private Transform _player;
    [SerializeField] private Transform _cameraAnchor;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void SpawnPlayer(Transform playerPos, Transform cameraPos, LevelInfo levelInfo)
    {
        _player.transform.position = playerPos.position;
        _cameraAnchor.transform.position = cameraPos.position;
        
        _player.transform.rotation = playerPos.rotation;
        _cameraAnchor.transform.rotation = playerPos.rotation;

        if(levelInfo._levelIndex == 2)
        {
            _player.GetComponent<TeleportAction>().enabled = false;
        }

        _player.GetComponent<FirstPersonCharacterController>().SetSpeed(levelInfo._playerSpeed);
    }

    public void ChangePlayerSize(float newSize)
    {
        _player.transform.localScale = new Vector3(newSize, newSize, newSize);
    }

    public void ChangePlayerRotation(Transform playerPos, Transform cameraPos)
    {
        _player.transform.rotation = playerPos.rotation;
        _cameraAnchor.transform.rotation = cameraPos.rotation;
    }
}
