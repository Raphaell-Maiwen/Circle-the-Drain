using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using Bloom = UnityEngine.Rendering.Universal.Bloom;

public class DarkWorld : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private Volume _volume;
    [SerializeField] private float _bloomIntensity;
    [SerializeField] private float _bloomScatter;
    [SerializeField] private float _lowPassCutoffFrequency;

    private float _initialBloomIntensity;
    private float _initialBloomScatter;
    private Bloom _bloom;

    [SerializeField] private float _darkWorldDuration;

    public void GetInDarkWorld()
    {
        _light.enabled = false;
        RenderSettings.ambientMode = AmbientMode.Skybox;
        _volume.profile.TryGet<Bloom>(out _bloom);
        
        _initialBloomIntensity = _bloom.intensity.value;
        _bloom.intensity.value = _bloomIntensity;
        _initialBloomScatter = _bloom.scatter.value;
        _bloom.scatter.value = _bloomScatter;
        
        AudioManager.Instance.AddLowPassFilter(_lowPassCutoffFrequency);

        StartCoroutine(LeaveDarkWorld());
    }

    IEnumerator LeaveDarkWorld()
    {
        yield return new WaitForSeconds(_darkWorldDuration);
        
        _light.enabled = true;
        _bloom.intensity.value = _initialBloomIntensity;
        _bloom.scatter.value = _initialBloomScatter;
        RenderSettings.ambientMode = AmbientMode.Flat;
        
        AudioManager.Instance.RemoveLowPassFilter();
    }
}
