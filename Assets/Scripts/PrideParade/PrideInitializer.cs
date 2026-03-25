using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PrideInitializer : LevelInitialization
{
    [SerializeField] private List<Animator> walkAnimatorList;
    [SerializeField] private List<Animator> waveAnimatorList;
    [SerializeField] private List<Animator> _dogAnimatorList;
    [SerializeField] private Animator _dykeLeadAnimator;
    [SerializeField] private string _transitionSong;
    [SerializeField] private string _nightmareSong;
    [SerializeField] private float _nightmareIncreaseRate;
    [SerializeField] private float _transitionDuration;
    
    [SerializeField] private Volume _volume;
    private VolumeProfile _profile;
    [SerializeField] private Light _light;

    [Header("Visual values")] 
    [SerializeField] private Color _endColor;
    [SerializeField] private float _endVignetteIntensity;
    [SerializeField] private float _endLightIntensity;
    [SerializeField] private float _startGrainIntensity;
    [SerializeField] private float _endGrainIntensity;

    [Header("Audio values")]
    [SerializeField] private float _startChorusRate;
    [SerializeField] private float _endChorusRate;
    [SerializeField] private float _startChorusDelay;
    [SerializeField] private float _endChorusDelay;
    [SerializeField] private float _startDistortionLevel;
    [SerializeField] private float _endDistortionLevel;

    private Coroutine _increaseNightmareCoroutine;

    void Start()
    {
        _profile = _volume.profile;
        
        base.Start();

        foreach (var animator in walkAnimatorList)
        {
            StartCoroutine(StartWalk(animator));
        }

        foreach (var animator in waveAnimatorList)
        {
            animator.SetTrigger("wave");
        }

        foreach (var animator in _dogAnimatorList)
        {
            animator.SetTrigger("walk");
        }

        _dykeLeadAnimator.SetTrigger("idle");

        StartCoroutine(StartNightmareTransition());
    }

    IEnumerator StartWalk(Animator animator)
    {
        yield return new WaitForSeconds(Random.Range(0, 1f));
        animator.SetTrigger("walk");
    }

    IEnumerator StartNightmareTransition()
    {
        AudioManager audioManager = AudioManager.Instance;

        while (audioManager.GetAudioSource(audioManager.GetLevelSong()).isPlaying)
        {
            yield return null;
        }

        foreach(var animator in walkAnimatorList)
        {
            animator.SetTrigger("walk");
        }

        audioManager.PlaySound(_transitionSong, false);
        audioManager.AddChorusFilter(_startChorusDelay, _startChorusRate);
        audioManager.AddAudioDistortionFilter(_startDistortionLevel);

        StartCoroutine(StartNightmare());
    }

    IEnumerator StartNightmare()
    {
        while (AudioManager.Instance.GetAudioSource(_transitionSong).isPlaying)
        {
            yield return null;
        }

        AudioManager.Instance.PlaySound(_nightmareSong, true);
        StartCoroutine(IncreaseNightmare(_transitionDuration));
    }

    IEnumerator IncreaseNightmare(float duration)
    {
        AudioManager audioManager = AudioManager.Instance;
        
        float elapsedTime = 0f;
        _volume.enabled = true;

        LiftGammaGain liftGammaGain;
        _volume.profile.TryGet<LiftGammaGain>(out liftGammaGain);
        Color startColor = liftGammaGain.gamma.value;
        
        Vignette vignette;
        _profile.TryGet<Vignette>(out vignette);
        
        FilmGrain filmGrain;
        _profile.TryGet<FilmGrain>(out filmGrain);

        float startLightIntensity = _light.intensity;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            
            //Visuals
            liftGammaGain.gamma.value = Color.Lerp(startColor, _endColor, t);
            vignette.intensity.value = Mathf.Lerp(0f, _endVignetteIntensity, t);
            filmGrain.intensity.value = Mathf.Lerp(_startGrainIntensity, _endGrainIntensity, t);
            _light.intensity = Mathf.Lerp(startLightIntensity, _endLightIntensity, t);
            
            //Audio
            audioManager.SetDistortionFilter(Mathf.Lerp(_startDistortionLevel, _endDistortionLevel, t));
            audioManager.SetChorusFilterDelay(Mathf.Lerp(_startChorusDelay, _endChorusDelay, t));
            audioManager.SetChorusFilterRate(Mathf.Lerp(_startChorusRate, _endChorusRate, t));
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        liftGammaGain.gamma.value = _endColor;
        vignette.intensity.value = _endVignetteIntensity;
        filmGrain.intensity.value = _endGrainIntensity;
        _light.intensity = _endLightIntensity;
        audioManager.SetDistortionFilter(_endDistortionLevel);
        audioManager.SetChorusFilterDelay(_endChorusDelay);
        audioManager.SetChorusFilterRate(_endChorusRate);
    }
}

































