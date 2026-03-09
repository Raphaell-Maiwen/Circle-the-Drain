using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Scriptable Objects/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    [SerializeField] public string _levelSong;
    [SerializeField] public bool _onLoop;
    [SerializeField] public GameObject _UIPrefab;
    [SerializeField] public Transform _cameraStartingAnchor;
    [SerializeField] public Transform _playerStartingAnchor;
}
