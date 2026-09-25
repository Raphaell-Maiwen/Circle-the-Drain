using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using Bloom = UnityEngine.Rendering.Universal.Bloom;
using ChromaticAberration = UnityEngine.Rendering.Universal.ChromaticAberration;

public class Trippy : MonoBehaviour
{
    [SerializeField] private float _tripDuration;
    [SerializeField] private float _tripBloomIntensity;
    [SerializeField] private float _chromaticAberrationIntensity;
    [SerializeField] private Volume _volume;
    private Bloom bloom;
    private ChromaticAberration _chromaticAberration;
    private float _initialIntensity;
    private float _initialChromaticAberrationIntensity;

    public void StartTrip()
    {
        AudioManager.Instance.AddReverbFilter(AudioReverbPreset.SewerPipe);
        _volume.profile.TryGet<Bloom>(out bloom);
        //_volume.profile.TryGet<ChromaticAberration>(out _chromaticAberration);
        
        _initialIntensity = bloom.intensity.value;
        //_initialChromaticAberrationIntensity = _chromaticAberration.intensity.value;
        bloom.intensity.value = _tripBloomIntensity;
        //_chromaticAberration.intensity.value = _chromaticAberrationIntensity;
        StartCoroutine(EndTrip());
    }

    IEnumerator EndTrip()
    {
        yield return new WaitForSeconds(_tripDuration);
        AudioManager.Instance.RemoveReverbFilter();
        
        _volume.profile.TryGet<Bloom>(out bloom);
        
        bloom.intensity.value = _initialIntensity;
        //_chromaticAberration.intensity.value = _initialChromaticAberrationIntensity;
    }
}
