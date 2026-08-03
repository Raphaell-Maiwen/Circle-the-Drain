using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class NumbersCutscene : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _speed;
    [SerializeField] private Subtitles _subtitlesKey;
    [SerializeField] private SubtitlesEventChannel _subtitlesEventChannel;

    [SerializeField] private bool _test;

    #if UNITY_EDITOR
    private void Start()
    {
        if (_test)
        {
            StartCoroutine(StartVideo());
        }
    }
#endif
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(StartVideo());
        }
    }

     IEnumerator StartVideo()
     {
         _renderer.enabled = true;
        _videoPlayer.enabled = true;
        _collider.enabled = false;

        yield return new WaitForSeconds(0.1f);
        yield return null;
        //Maybe just a yield return null?

        _subtitlesEventChannel.SubtitlesStarted(_subtitlesKey);
        while (_videoPlayer.isPlaying)
        {
            transform.position += transform.up * _speed * Time.deltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}



















