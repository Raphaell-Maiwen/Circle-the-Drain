using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using static TVStaticManager;

public class televisionCode : MonoBehaviour
{
    public bool Fix;
    [Range(0, 50)] public int hp;
    [Range(0, 1)] public int usedStaticClip;
    public bool UseStatic;
    [Tooltip("Main model")] public GameObject screen;
    [Tooltip("Video playing model")] public GameObject screenVideoParent;
    [Tooltip("When hp = 0")]public GameObject sparksEffect;
    [Tooltip("Main,Broken")] public Material[] screenMaterials;
    public VideoClip[] staticVideos;
    public VideoClip[] tvVideoClips;
    [HideInInspector]public bool inRange;
    bool broken;
    bool onOffBool = false;
    int onOffInt = 0;
    int onOffIntTARGET = 100;
    bool cooldownOn = false;
    int lastRandomInt = -1;

    public VideoPlayer TVPlayer { get; private set; }
    private Renderer _renderer;

    private int _videoIndex = 0;

    private void Awake()
    {
        TVPlayer = screenVideoParent.GetComponent<VideoPlayer>();
        _renderer = screenVideoParent.GetComponent<Renderer>();
    }

    void Start()
    {
        if (hp == 0) { screen.GetComponent<Renderer>().material = screenMaterials[1]; }
        else { screen.GetComponent<Renderer>().material = screenMaterials[0]; }
    }
    //------------------------------------------------------------------------------------------------------------------------------------------
    
    public void on(){
        if (UseStatic == false) { channelChange(); } else if (UseStatic == true) { tvStatic(); }
        StartCoroutine(Delay());
        onOffBool = !onOffBool;
    }
    public void off(){
        screenVideoParent.SetActive(false);
        TVPlayer.Stop();
        StartCoroutine(Delay());
        onOffBool = !onOffBool;
    }
    public void tvStatic()
    {
        screenVideoParent.SetActive(true);
        TVPlayer.clip = null;
        
        //vp.targetTexture = Instance.staticRenderTexture;
        
        TVPlayer.Stop(); // No need to play, the RenderTexture is already being written to

        // Apply the render texture to the screen material directly
        _renderer.material.mainTexture = Instance.staticRenderTexture;
        StartCoroutine(Delay());
    }

    public void channelChange()
    {
        TVPlayer.targetTexture = null;
        TVPlayer.renderMode = VideoRenderMode.MaterialOverride;
        
        screenVideoParent.SetActive(true);
        TVPlayer.Stop();

        if (_videoIndex >= tvVideoClips.Length)
        {
            Debug.LogError("You are trying to access more videos than there are!");
            _videoIndex = tvVideoClips.Length - 1;
        }

        TVPlayer.clip = tvVideoClips[_videoIndex];
        TVPlayer.Play();
        
        _videoIndex++;
    }

    public void SetLoop(bool isLooping)
    {
        TVPlayer.isLooping = isLooping;
    }

    public void breakAction(){
        sparksEffect.SetActive(true);
        screen.GetComponent<Renderer>().material = screenMaterials[1];
        TVPlayer.enabled = false;
        screenVideoParent.SetActive(false);
    }
    IEnumerator Delay() {
        cooldownOn = true;
        yield return new WaitForSeconds(1f);
        cooldownOn = false;
    }
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){ inRange = true; }
    }
    private void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Player"){ inRange = false; }
    }
}