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

    private void Start()
    {
        StartCoroutine(CheckCutsceneEnded());
    }

    IEnumerator CheckCutsceneEnded()
    {
        yield return new WaitForSeconds(2f);
        
        while(_videoPlayer.isPlaying) yield return null;
        
        SceneManager.LoadScene(_creditsScene);
    }
}
