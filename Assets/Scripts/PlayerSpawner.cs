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

    public void SpawnPlayer(Transform playerPos, Transform cameraPos)
    {
        _player.transform.position = playerPos.position;
        _cameraAnchor.transform.position = cameraPos.position;
    }
}
