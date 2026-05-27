using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndOfLevel2Trigger : MonoBehaviour
{
    [SerializeField] private GameObject[] _videoPlayerArray;
    [SerializeField] private float _finalHeight;
    [SerializeField] private float _riseLength;
    [SerializeField] private PrideParadeProgress _prideParadeProgress;
    
    private List<VideoWall> _videoWallList = new List<VideoWall>();

    private void Start()
    {
        foreach (var videoPlayer in _videoPlayerArray)
        {
            var startingPos = videoPlayer.transform.position;
            var endingPos = videoPlayer.transform.position;
            endingPos.y = _finalHeight;
            
            _videoWallList.Add(new VideoWall(videoPlayer, startingPos, endingPos));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndOfParade")
        {
            StartCoroutine(EndOfParade());
        }
    }

    private IEnumerator EndOfParade()
    {
        foreach (var videoWall in _videoWallList)
        {
            videoWall._videoPlayer.gameObject.SetActive(true);
        }
        
        float elapsed = 0f;
        
        while(elapsed < _riseLength)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _riseLength);

            foreach (var videoWall in _videoWallList)
            {
                videoWall._videoPlayer.transform.position = Vector3.Lerp(videoWall._startingPos, videoWall._endingPos, t);
            }

            yield return null;
        }
        
        foreach (var videoWall in _videoWallList)
        {
            videoWall._videoPlayer.transform.position = videoWall._endingPos;
        }
        
        _prideParadeProgress.EndReached();
    }
    
    private struct VideoWall
    {
        public GameObject _videoPlayer;
        public Vector3 _startingPos;
        public Vector3 _endingPos;

        public VideoWall(GameObject videoPlayer, Vector3 startingPos, Vector3 endingPos)
        {
            _videoPlayer = videoPlayer;
            _startingPos = startingPos;
            _endingPos = endingPos;
        }
    }
}











