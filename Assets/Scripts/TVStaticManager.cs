using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVStaticManager : MonoBehaviour
{
    [SerializeField] private HauntedHouseProgress _progress;
    [SerializeField] private televisionCode _televisionMaster;
    public static TVStaticManager Instance;
    public VideoClip staticClip;
    public RenderTexture staticRenderTexture;
    private VideoPlayer _videoPlayer;

    [SerializeField] private List<televisionCode> _televisionsList;
    [SerializeField] private int[] _videosOrder;
    
    private int _videoIndex;

    void Awake()
    {
        Instance = this;
        _videoPlayer = gameObject.AddComponent<VideoPlayer>();
        _videoPlayer.clip = staticClip;
        _videoPlayer.targetTexture = staticRenderTexture;
        _videoPlayer.isLooping = true;

        _progress.OnAllBooksRead += TurnOnTVs;
        
        //For testing purposes
        TurnOnTVs();
        PlayAlienCutscene();
    }

    public void TurnOnTVs()
    {
        _videoPlayer.Play();

        foreach (var television in _televisionsList)
        {
            television.tvStatic();
        }
    }

    public void PlayAlienCutscene()
    {
        //Close door
        StartCoroutine(PlayVideoSequence());
    }

    IEnumerator PlayVideoSequence()
    {
        televisionCode tv;
        
        while (_videoIndex < _videosOrder.Length)
        {
            tv = _televisionsList[_videosOrder[_videoIndex]];
            tv.channelChange();
            
            yield return new WaitForSeconds(1f);
        
            while (tv.TVPlayer.isPlaying)
            {
                yield return null;
            }
            
            _videoIndex++;
            _televisionsList[_videosOrder[_videoIndex - 1]].tvStatic();
        }
        
        //Open door
    }
}

















