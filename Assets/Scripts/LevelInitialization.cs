using UnityEngine;

public class LevelInitialization : MonoBehaviour
{
    [SerializeField] private string _levelSong;
    [SerializeField] private AudioManager _audioManager;

    void Start()
    {
        _audioManager.PlaySound(_levelSong, true);
    }
}
