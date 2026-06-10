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
    private VideoPlayer videoPlayer;

    [SerializeField] private List<televisionCode> _televisionsList;
    [SerializeField] private int[] _videosOrder;
    
    private int _videoIndex;

    void Awake()
    {
        Instance = this;
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.clip = staticClip;
        videoPlayer.targetTexture = staticRenderTexture;
        videoPlayer.isLooping = true;

        _progress.OnAllBooksRead += TurnOnTVs;
        
        //For testing purposes
        TurnOnTVs();
        PlayAlienCutscene();
    }

    public void TurnOnTVs()
    {
        videoPlayer.Play();

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
        var tv = _televisionsList[_videosOrder[_videoIndex]];
        tv.channelChange();
        _videoIndex++;
        
        //Have a variable for that in televisionCode
        var videoPlayer = tv.screenVideoParent.GetComponent<VideoPlayer>();

        yield return new WaitForSeconds(1f);
        
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
        
        tv = _televisionsList[_videosOrder[_videoIndex]];
        tv.channelChange();
        
        _televisionsList[_videosOrder[_videoIndex - 1]].tvStatic();
        _videoIndex++;
        
        videoPlayer = tv.screenVideoParent.GetComponent<VideoPlayer>();
        
        yield return new WaitForSeconds(1f);
        
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
        
        tv = _televisionsList[_videosOrder[_videoIndex]];
        tv.channelChange();
        _televisionsList[_videosOrder[_videoIndex - 1]].tvStatic();

        //Repasser le static sur tout le monde? Ou genre, garder en tête le précédent: oui
        
        //Open door
    }
}

















