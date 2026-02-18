using UnityEngine;

public class LevelInitialization : MonoBehaviour
{
    [SerializeField] private string _levelSong;

    void Start()
    {
        AudioManager.Instance.PlaySound(_levelSong, true);
    }
}
