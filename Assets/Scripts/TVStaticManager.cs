using System;
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

    [SerializeField] private GameObject _enterDoor;
    [SerializeField] private GameObject _exitDoor;
    
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
        /*TurnOnTVs();
        PlayAlienCutscene();*/
    }

    #if UNITY_EDITOR
    private void Start()
    {
        if (_progress.StartWithAllBooks)
        {
            _progress.TestTVRoom();
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
    }
}

















