using UnityEngine;
using UnityEngine.Video;

public class TVStaticManager : MonoBehaviour
{
    [SerializeField] private televisionCode _televisionMaster;
    public static TVStaticManager Instance;
    public VideoClip staticClip;
    public RenderTexture staticRenderTexture;
    private VideoPlayer videoPlayer;

    void Awake()
    {
        Instance = this;
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.clip = staticClip;
        videoPlayer.targetTexture = staticRenderTexture;
        videoPlayer.isLooping = true;
        videoPlayer.Play();
        
        _televisionMaster.tvStatic();
    }
}