using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SubtitlesEventChannel", menuName = "Scriptable Objects/SubtitlesEventChannel")]
public class SubtitlesEventChannel : ScriptableObject
{
    public event Action<string> OnSubtitlesStarted;
    public event Action<string> OnSubtitlesUpdated;

    public void SubtitlesStarted(string key)
    {
        OnSubtitlesStarted?.Invoke(key);
    }

    public void UpdateSubtitles(string text)
    {
        OnSubtitlesUpdated?.Invoke(text);
    }
}
