using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SubtitlesEventChannel", menuName = "Scriptable Objects/SubtitlesEventChannel")]
public class SubtitlesEventChannel : ScriptableObject
{
    public event Action<string> OnSubtitlesUpdated;

    public void UpdateSubtitles(string text)
    {
        OnSubtitlesUpdated?.Invoke(text);
    }
}
