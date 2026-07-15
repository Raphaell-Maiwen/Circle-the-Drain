using UnityEngine;
using UnityEngine.Video;

public class WomansWorldStopTrigger : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _videoPlayer.isPlaying)
        {
            _videoPlayer.Pause();
        }
    }
}
