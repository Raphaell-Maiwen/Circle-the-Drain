using UnityEngine;

public class LevelInitialization : MonoBehaviour
{
    [SerializeField] private string _levelSong;
    [SerializeField] private AudioManagerMessenger _audioManagerMessenger;

    void Start()
    {
        _audioManagerMessenger.AudioManager.PlaySound(_levelSong, true);
    }
}
