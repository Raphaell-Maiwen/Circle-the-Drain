using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using Bloom = UnityEngine.Rendering.Universal.Bloom;

public class Trippy : MonoBehaviour
{
    [SerializeField] private float _tripDuration;
    [SerializeField] private float _tripBloomIntensity;
    [SerializeField] private Volume _volume;
    private Bloom bloom;
    private float _initialIntensity;

    public void StartTrip()
    {
        AudioManager.Instance.AddReverbFilter(AudioReverbPreset.SewerPipe);
        _volume.profile.TryGet<Bloom>(out bloom);
        
        _initialIntensity = bloom.intensity.value;
        bloom.intensity.value = _tripBloomIntensity;
        StartCoroutine(EndTrip());
    }

    IEnumerator EndTrip()
    {
        yield return new WaitForSeconds(_tripDuration);
        AudioManager.Instance.RemoveReverbFilter();
        
        _volume.profile.TryGet<Bloom>(out bloom);
        
        bloom.intensity.value = _initialIntensity;
    }
}
