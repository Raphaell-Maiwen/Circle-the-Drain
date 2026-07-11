using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitlesManager : MonoBehaviour
{
    [SerializeField] private SubtitlesEventChannel _channel;
    //For testing purposes
    [SerializeField] private Subtitles _currentSubtitles;
    [SerializeField] private PlayableDirector _director;

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
        _director.Play();
    }

    public void TestTimeline()
    {
        Debug.Log("Test " + Time.time);
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
