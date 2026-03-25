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
    [SerializeField] private float _vignetteIntensity;
    [SerializeField] private float _lightIntensity;
    [SerializeField] private float _startGrainIntensity;
    [SerializeField] private float _endGrainIntensity;

    private Coroutine _increaseNightmareCoroutine;

    void Start()
    {
        _profile = _volume.profile;
        
        Invoke(nameof(TestFilterFunction), 5f);
        
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

    private void TestFilterFunction()
    {
        Color endColor = new Vector4(0.75f, 0.76f, 1.00f, 0.23f);

        Debug.Log("Start visual changes");
        
        StartCoroutine(IncreaseNightmareVisuals(_transitionDuration));
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
        audioManager.AddChorusFilter(5f, 1f);
        audioManager.AddAudioDistortionFilter(0.3f);

        StartCoroutine(StartNightmare());
    }

    IEnumerator StartNightmare()
    {
        while (AudioManager.Instance.GetAudioSource(_transitionSong).isPlaying)
        {
            yield return null;
        }

        AudioManager.Instance.PlaySound(_nightmareSong, true);
        StartCoroutine(IncreaseNightmare());
    }

    IEnumerator IncreaseNightmare()
    {
        bool rateCapReached = AudioManager.Instance.AddChorusFilter(0f, 2f, 100f, 10f);
        bool distortionCapReached = AudioManager.Instance.AddAudioDistortionFilter(0.1f, 0.6f);

        if (rateCapReached && distortionCapReached)
        {
            StopCoroutine(_increaseNightmareCoroutine);
        }

        yield return new WaitForSeconds(_nightmareIncreaseRate);
        _increaseNightmareCoroutine = StartCoroutine(IncreaseNightmare());
    }

    IEnumerator IncreaseNightmareVisuals(float duration)
    {
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
            
            liftGammaGain.gamma.value = Color.Lerp(startColor, _endColor, t);
            vignette.intensity.value = Mathf.Lerp(0f, _vignetteIntensity, t);
            filmGrain.intensity.value = Mathf.Lerp(_startGrainIntensity, _endGrainIntensity, t);
            _light.intensity = Mathf.Lerp(startLightIntensity, _lightIntensity, t);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        liftGammaGain.gamma.value = _endColor;
    }
}

































