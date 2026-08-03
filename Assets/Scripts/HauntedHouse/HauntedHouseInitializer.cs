using UnityEngine;
using UnityEngine.Video;

public class HauntedHouseInitializer: LevelInitialization
{
    [SerializeField] private float _playerShrinkedSize;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private HauntedHouseProgress _progress;

    void Start()
    {
        base.Start();
        
        PlayerSpawner.Instance.ChangePlayerSize(_playerShrinkedSize);
        PlayerSpawner.Instance.ChangePlayerRotation(_levelInfo._playerStartingAnchor, _levelInfo._cameraStartingAnchor);
        
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        _videoPlayer.SetTargetAudioSource(0, _audioSource);
        
        _videoPlayer.Play();
        
        _progress.Reset();
    }
}