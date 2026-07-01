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

    private void Start()
    {
        //Test
        StartSubtitles("test");
    }

    public void StartSubtitles(string key)
    {
        _index = 0;
        StartCoroutine(ChangeSubtitles(key));
    }

    IEnumerator ChangeSubtitles(string key)
    {
        var subtitlesData = _subtitles[key]._subtitlesData;
        
        if (_index >= subtitlesData.Count)
        {
            _channel.UpdateSubtitles("");
        }
        else
        {
            _channel.UpdateSubtitles(subtitlesData[_index].Text);
            yield return new WaitForSeconds(subtitlesData[_index].Duration);
            _index++;
            StartCoroutine(ChangeSubtitles(key));
        }
    }
}
