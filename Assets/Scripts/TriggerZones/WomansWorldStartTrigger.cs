using UnityEngine;
using UnityEngine.Video;

public class WomansWorldStartTrigger : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _videoPlayer.Play();
        }
    }
}
