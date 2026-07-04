using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class SubtitlesManager : MonoBehaviour
{
    //Identifier avec le SO direct?
    [SerializeField] private SerializedDictionary<string, Subtitles> _subtitles;
    [SerializeField] private SubtitlesEventChannel _channel;

    private int _index;

    private void OnEnable()
    {
        _channel.OnSubtitlesStarted += StartSubtitles;
    }

    private void OnDisable()
    {
        _channel.OnSubtitlesStarted -= StartSubtitles;
    }

    public void StartSubtitles(string key)
    {
        _index = 0;
    }

    public void UpdateSubtitles(string key)
    {
        var subtitlesData = _subtitles[key]._subtitlesData;
        
        if (_index >= subtitlesData.Count)
        {
            _channel.UpdateSubtitles("");
        }
        else
        {
            _channel.UpdateSubtitles(subtitlesData[_index].Text);
            _index++;
        }
    }
}
