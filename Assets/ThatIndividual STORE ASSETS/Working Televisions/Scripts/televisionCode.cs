using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace WorkingTelevisions
{
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
        void Start()
        {
            if (hp == 0) { screen.GetComponent<Renderer>().material = screenMaterials[1]; }
            else { screen.GetComponent<Renderer>().material = screenMaterials[0]; }
        }
        /*void Update()
        {
            if (hp != 0 && broken == false) {  }
            if (hp == 0 && broken == false) { breakAction(); broken = true; screenVideoParent.SetActive(false); }
            if(Fix == true) { 
                Fix = false;
                broken = false;
                hp = 50;
                sparksEffect.SetActive(false);
                screen.GetComponent<Renderer>().material = screenMaterials[0];
                screenVideoParent.SetActive(false);
                screenVideoParent.GetComponent<VideoPlayer>().enabled = true;
            }
            //---
            if(inRange == true && Input.GetKeyDown(KeyCode.F) && broken == false) { onOffInt = 0; }
            if(inRange == true && Input.GetKey(KeyCode.F) && broken == false) {
                if(cooldownOn == false){ onOffInt++; }
            }
            if (inRange == true && broken == false){
                if(Input.GetKey(KeyCode.F) && cooldownOn == false && onOffInt >= onOffIntTARGET){
                    if (onOffBool == false) { on(); }
                    else if (onOffBool == true) { off(); }
                }
                if (Input.GetKeyUp(KeyCode.F) && onOffInt < onOffIntTARGET && onOffBool == true) { channelChange(); }
            }
        }*/
        //------------------------------------------------------------------------------------------------------------------------------------------
        
        public void on(){
            if (UseStatic == false) { channelChange(); } else if (UseStatic == true) { tvStatic(); }
            StartCoroutine(Delay());
            onOffBool = !onOffBool;
        }
        public void off(){
            screenVideoParent.SetActive(false);
            screenVideoParent.GetComponent<VideoPlayer>().Stop();
            StartCoroutine(Delay());
            onOffBool = !onOffBool;
        }
        public void tvStatic()
        {
            screenVideoParent.SetActive(true);
            screenVideoParent.GetComponent<VideoPlayer>().clip = staticVideos[usedStaticClip];
            screenVideoParent.GetComponent<VideoPlayer>().Play();
            StartCoroutine(Delay());
        }
        public void channelChange()
        {
            //play channel change audio clip
            screenVideoParent.SetActive(true);
            screenVideoParent.GetComponent<VideoPlayer>().Stop();
            if(UseStatic == false) {
                int rand = Random.Range(0, tvVideoClips.Length);
                while(rand == lastRandomInt) { rand = Random.Range(0, tvVideoClips.Length); }
                screenVideoParent.GetComponent<VideoPlayer>().clip = tvVideoClips[rand];
                lastRandomInt = rand;
            }
            else if (UseStatic == true) { screenVideoParent.GetComponent<VideoPlayer>().clip = staticVideos[usedStaticClip]; }
            screenVideoParent.GetComponent<VideoPlayer>().Play();
        }
        public void breakAction(){
            sparksEffect.SetActive(true);
            screen.GetComponent<Renderer>().material = screenMaterials[1];
            screenVideoParent.GetComponent<VideoPlayer>().enabled = false;
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
}