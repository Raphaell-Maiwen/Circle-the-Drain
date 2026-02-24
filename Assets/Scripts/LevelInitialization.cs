using UnityEngine;

public class LevelInitialization : MonoBehaviour
{
    [SerializeField] private string _levelSong;
    [SerializeField] private bool _onLoop;

    void Start()
    {
        AudioManager.Instance.PlaySound(_levelSong, _onLoop);
    }
}
