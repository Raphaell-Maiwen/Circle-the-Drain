using UnityEngine;

[CreateAssetMenu(fileName = "AudioManagerMessenger", menuName = "Scriptable Objects/AudioManagerMessenger")]
public class AudioManagerMessenger : ScriptableObject
{
    [SerializeField] private AudioManager _audioManager;
    public AudioManager AudioManager => _audioManager;
}
