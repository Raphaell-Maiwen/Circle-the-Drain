using UnityEngine;

public class LevelInitialization : MonoBehaviour
{
    [SerializeField] protected string _levelSong;
    [SerializeField] protected bool _onLoop;

    protected void Start()
    {
        AudioManager.Instance.PlaySound(_levelSong, _onLoop);
    }
}
