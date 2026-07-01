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
    [SerializeField] private float _duration;
    
    public string Text => _text;
    public float Duration => _duration;
}
