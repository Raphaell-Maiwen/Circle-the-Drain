using System;
using TMPro;
using UnityEngine;

public class FinalCutsceneUI : MonoBehaviour
{
    [SerializeField] private SubtitlesEventChannel _subtitlesEventChannel;
    [SerializeField] protected TextMeshProUGUI _subtitlesText;

    private void OnEnable()
    {
        _subtitlesEventChannel.OnSubtitlesUpdated += ShowSubtitles;
    }

    private void OnDisable()
    {
        _subtitlesEventChannel.OnSubtitlesUpdated -= ShowSubtitles;
    }

    public void ShowSubtitles(string text)
    {
        _subtitlesText.text = text;
    }
}
