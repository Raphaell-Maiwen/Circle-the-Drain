using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "Subtitles", menuName = "Scriptable Objects/Subtitles")]
public class Subtitles : ScriptableObject
{
    public List<SubtitlesData> _subtitlesData;
}

[Serializable]
public class SubtitlesData
{
    [SerializeField] private string _text;
    
    public string Text => _text;

    [SerializeField] private double _timeStamp = 100;
    public double TimeStamp => _timeStamp;
}
