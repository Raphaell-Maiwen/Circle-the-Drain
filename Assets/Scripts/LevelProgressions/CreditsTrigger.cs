using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CreditsTrigger : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private string _creditsScene;

    [SerializeField] private Subtitles _subtitlesKey;
    [SerializeField] private SubtitlesEventChannel _subtitlesEventChannel;

    private void Start()
    {
        StartCoroutine(CheckCutsceneEnded());
        _subtitlesEventChannel.SubtitlesStarted(_subtitlesKey);
    }

    IEnumerator CheckCutsceneEnded()
    {
        yield return new WaitForSeconds(2f);
        
        while(_videoPlayer.isPlaying) yield return null;

        Destroy(GameObject.Find("ProgressMessagesHaunted"));
        SceneManager.LoadScene(_creditsScene);
    }
}
