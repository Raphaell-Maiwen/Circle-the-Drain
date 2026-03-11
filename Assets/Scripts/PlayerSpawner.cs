using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance { get; private set; }

    [SerializeField] private Transform _player;
    [SerializeField] private Transform _cameraAnchor;
    private bool _level2;

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

    public void SpawnPlayer(Transform playerPos, Transform cameraPos, int levelIndex)
    {
        _player.transform.position = playerPos.position;
        _cameraAnchor.transform.position = cameraPos.position;

        if(levelIndex == 2)
        {
            _player.GetComponent<TeleportAction>().enabled = false;
        }
    }
}
