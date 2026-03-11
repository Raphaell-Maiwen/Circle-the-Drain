using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Scriptable Objects/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    [SerializeField] public int _levelIndex;
    [SerializeField] public string _levelSong;
    [SerializeField] public bool _onLoop;
    [SerializeField] public GameObject _UIPrefab;
    [SerializeField] public Transform _cameraStartingAnchor;
    [SerializeField] public Transform _playerStartingAnchor;
    [SerializeField] public float _playerSpeed;

    //Maybe awake function here???
}
