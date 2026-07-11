using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SubtitlesEventChannel", menuName = "Scriptable Objects/SubtitlesEventChannel")]
public class SubtitlesEventChannel : ScriptableObject
{
    public event Action<Subtitles> OnSubtitlesStarted;
    public event Action<string> OnSubtitlesUpdated;

    public void SubtitlesStarted(Subtitles subtitles)
    {
        OnSubtitlesStarted?.Invoke(subtitles);
    }

    public void UpdateSubtitles(string text)
    {
        OnSubtitlesUpdated?.Invoke(text);
    }
}
