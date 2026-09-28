using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using Bloom = UnityEngine.Rendering.Universal.Bloom;

public class DarkWorld : MonoBehaviour
{
    [SerializeField] private VolumeDefaultValues _volumeDefaultValues;
    
    [SerializeField] private Light _light;
    [SerializeField] private Volume _volume;
    [SerializeField] private float _bloomIntensity;
    [SerializeField] private float _bloomScatter;
    [SerializeField] private float _lowPassCutoffFrequency;
    
    private Bloom _bloom;

    [SerializeField] private float _darkWorldDuration;

    public void GetInDarkWorld()
    {
        _light.enabled = false;
        RenderSettings.ambientMode = AmbientMode.Skybox;
        _volume.profile.TryGet<Bloom>(out _bloom);
        _bloom.intensity.value = _bloomIntensity;
        _bloom.scatter.value = _bloomScatter;
        
        AudioManager.Instance.AddLowPassFilter(_lowPassCutoffFrequency);

        StartCoroutine(LeaveDarkWorld());
    }

    IEnumerator LeaveDarkWorld()
    {
        yield return new WaitForSeconds(_darkWorldDuration);
        
        _light.enabled = true;
        _bloom.intensity.value = _volumeDefaultValues.BloomIntensity;
        _bloom.scatter.value = _volumeDefaultValues.BloomScatter;
        RenderSettings.ambientMode = AmbientMode.Flat;
        
        AudioManager.Instance.RemoveLowPassFilter();
    }
}
