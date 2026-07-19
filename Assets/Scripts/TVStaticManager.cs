using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVStaticManager : MonoBehaviour
{
    [SerializeField] private HauntedHouseProgress _progress;
    [SerializeField] private Subtitles _subtitlesKey;
    [SerializeField] private SubtitlesEventChannel _subtitlesEventChannel;
    [SerializeField] private televisionCode _televisionMaster;
    public static TVStaticManager Instance;
    public VideoClip staticClip;
    public RenderTexture staticRenderTexture;
    private VideoPlayer _videoPlayer;

    [SerializeField] private List<televisionCode> _televisionsList;
    [SerializeField] private int[] _videosOrder;

    [SerializeField] private GameObject _enterDoor;
    [SerializeField] private GameObject _exitDoor;
    [SerializeField] private List<GameObject> _objectsToDisable;
    
    private int _videoIndex;

    [Header("Debugging variables")] [SerializeField]
    private bool _videosOnStart;

    void Awake()
    {
        Instance = this;
        _videoPlayer = gameObject.AddComponent<VideoPlayer>();
        _videoPlayer.clip = staticClip;
        _videoPlayer.targetTexture = staticRenderTexture;
        _videoPlayer.isLooping = true;
        _videoPlayer.SetDirectAudioMute(0, true);

        _progress.OnAllBooksRead += TurnOnTVs;
    }

    #if UNITY_EDITOR
    private void Start()
    {
        if (_progress.StartWithAllBooks)
        {
            _progress.TestTVRoom();
        }
        
        if (_videosOnStart)
        {
            TurnOnTVs();
            PlayAlienCutscene();
        }
    }
    #endif

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
        _enterDoor.SetActive(true);

        foreach (var obj in _objectsToDisable)
        {
            obj.SetActive(false);
        }
        
        _subtitlesEventChannel.SubtitlesStarted(_subtitlesKey);
        StartCoroutine(PlayVideoSequence());
    }

    IEnumerator PlayVideoSequence()
    {
        televisionCode tv;
        televisionCode previousTv;
        
        while (_videoIndex < _videosOrder.Length)
        {
            tv = _televisionsList[_videosOrder[_videoIndex]];
            tv.channelChange();
            tv.SetLoop(false);
            
            yield return new WaitForSeconds(1f);
        
            while (tv.TVPlayer.isPlaying)
            {
                yield return null;
            }
            
            _videoIndex++;
            previousTv = _televisionsList[_videosOrder[_videoIndex - 1]];
            previousTv.tvStatic();
            previousTv.SetLoop(true);
        }
        
        _enterDoor.SetActive(false);
        _exitDoor.SetActive(false);
        
        foreach (var obj in _objectsToDisable)
        {
            obj.SetActive(true);
        }
    }
}

















