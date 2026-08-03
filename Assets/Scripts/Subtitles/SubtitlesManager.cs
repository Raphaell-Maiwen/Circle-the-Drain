using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SubtitlesManager : MonoBehaviour
{
    [SerializeField] private SubtitlesEventChannel _channel;
    //For testing purposes
    [SerializeField] private Subtitles _currentSubtitles;
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private TimelineAsset _timeline;
    
    [SerializeField] private SignalAsset _subtitleSignalAsset;

    private int _index;

    //Test
    /*private void Start()
    {
        _channel.OnSubtitlesStarted += StartSubtitles;
        StartSubtitles(_currentSubtitles);
    }*/

    private void OnEnable()
    {
        _channel.OnSubtitlesStarted += StartSubtitles;
    }

    private void OnDisable()
    {
        _channel.OnSubtitlesStarted -= StartSubtitles;
    }

    public void StartSubtitles(Subtitles subtitles)
    {
        _index = 0;
        _currentSubtitles = subtitles;
        
        PopulateTimeline();
        
        _director.Play();
    }

    private void PopulateTimeline()
    {
        var signalTrack = _timeline.GetRootTrack(0);

        foreach (var mark in signalTrack.GetMarkers().ToList())
        {
            signalTrack.DeleteMarker(mark);
        }

        foreach (var subs in _currentSubtitles._subtitlesData)
        {
            var marker = signalTrack.CreateMarker<SignalEmitter>(subs.TimeStamp);
            marker.name = "NewSubtitle";
            marker.asset = _subtitleSignalAsset;
        }
        
        _director.RebuildGraph();
    }

    public void UpdateSubtitles()
    {
        var subtitlesData = _currentSubtitles._subtitlesData;
        
        if (_index >= subtitlesData.Count)
        {
            _channel.UpdateSubtitles("");
            _director.Stop();
        }
        else
        {
            _channel.UpdateSubtitles(subtitlesData[_index].Text);
            _index++;
        }
    }
}
