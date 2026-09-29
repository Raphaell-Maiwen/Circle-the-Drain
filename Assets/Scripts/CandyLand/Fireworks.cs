using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Bloom = UnityEngine.Rendering.Universal.Bloom;
using ChromaticAberration = UnityEngine.Rendering.Universal.ChromaticAberration;

public class Fireworks : MonoBehaviour
{
    [SerializeField] private string _fireworksSong;
    [SerializeField] private string _prideParadeLevel;
    [SerializeField] private string _candyLandLevel;
    [SerializeField] private float _fireworksShowDuration;
    [SerializeField] private LODGroup _lodGroup;
    [SerializeField] private List<GameObject> _objectsToDeactivate;
    [SerializeField] private List<GameObject> _objectsToActivate;
    
    [SerializeField] private Volume _volume;
    [SerializeField] private VolumeDefaultValues _volumeDefaultValues;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public IEnumerator InitializeFireworks()
    {
        AudioManager.Instance.PlaySound(_fireworksSong);

        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = Color.black;

        RemoveRocketsEffects();
        
        yield return new WaitForSeconds(1f);

        while (CamerasManager.CameraBrain.IsBlending)
        {
            yield return null;
        }

        foreach (var obj in _objectsToDeactivate)
        {
            obj.gameObject.SetActive(false);
        }

        foreach (var obj in _objectsToActivate)
        {
            obj.gameObject.SetActive(true);
        }
        
        SceneManager.UnloadSceneAsync(_candyLandLevel);

        StartCoroutine(LoadPrideParade());
    }

    //Very harcoded, but that's life
    //Improvement: add delegates to call (from the Rocket scripts)
    private void RemoveRocketsEffects()
    {
        //How to enable light?? Necessary?
        Bloom bloom;
        ChromaticAberration _chromaticAberration;
        
        _volume.profile.TryGet<Bloom>(out bloom);
        _volume.profile.TryGet<ChromaticAberration>(out _chromaticAberration);
        
        bloom.intensity.value = _volumeDefaultValues.BloomIntensity;
        bloom.scatter.value = _volumeDefaultValues.BloomScatter;
        _chromaticAberration.intensity.value = _volumeDefaultValues.ChromaticAberrationIntensity;

        foreach (var coroutine in _volumeDefaultValues.CoroutinesToStop)
        {
            StopCoroutine(coroutine);
        }
        
        RenderSettings.ambientMode = AmbientMode.Flat;
        
        AudioManager.Instance.RemoveReverbFilter();
        AudioManager.Instance.RemoveLowPassFilter();
    }

    IEnumerator LoadPrideParade()
    {
        yield return new WaitForSeconds(_fireworksShowDuration);

        _lodGroup.enabled = true;
        AudioManager.Instance.StopAllSounds();
        SceneManager.LoadSceneAsync(_prideParadeLevel, LoadSceneMode.Additive);

        StartCoroutine(EndLevelTransition());
    }

    IEnumerator EndLevelTransition()
    {
        yield return new WaitForSeconds(2f);

        CamerasManager.SwitchActiveCamera(CamerasManager.MainCamera);

        yield return new WaitForSeconds(1f);

        while (CamerasManager.CameraBrain.IsBlending)
        {
            yield return null;
        }

        CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Player");

        Destroy(this.gameObject);
    }
}
