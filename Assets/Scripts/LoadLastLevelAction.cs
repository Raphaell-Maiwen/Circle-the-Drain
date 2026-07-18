using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLastLevelAction : MonoBehaviour
{
    [SerializeField] private PrideParadeProgress _prideParadeProgress;
    [SerializeField] private InteractMessenger _interactMessenger;
    [SerializeField] private InteractableTriggerZoneEventChannel[] _zoneChannels;

    [SerializeField] private float _targetFocalLength;
    private float _initialFocalLength;
    [SerializeField] private float _focalLengthLerpDuration;

    [SerializeField] private string _prideLevel;
    [SerializeField] private string _alienLabLevel;

    private void OnEnable()
    {
        _prideParadeProgress.OnEndReached += EnableLoadLastLevel;
        
        if(_prideParadeProgress.IsEndReached) EnableLoadLastLevel();

        foreach (var zone in _zoneChannels)
        {
            zone.OnPlayerEntered += DisableLoadLastLevel;
            zone.OnPlayerExited += TryRestoreLoadLastLevel;
        }
    }

    private void OnDisable()
    {
        _prideParadeProgress.OnEndReached -= EnableLoadLastLevel;

        foreach (var zone in _zoneChannels)
        {
            zone.OnPlayerEntered -= DisableLoadLastLevel;
            zone.OnPlayerExited -= TryRestoreLoadLastLevel;
        }

        _interactMessenger.OnInteractInput -= LoadLastLevel;
    }

    private void EnableLoadLastLevel() => _interactMessenger.OnInteractInput += LoadLastLevel;
    private void DisableLoadLastLevel(InteractableTriggerZoneEventChannel channel) => _interactMessenger.OnInteractInput -= LoadLastLevel;

    private void TryRestoreLoadLastLevel()
    {
        _interactMessenger.OnInteractInput -= LoadLastLevel;
        if(_prideParadeProgress.IsEndReached) EnableLoadLastLevel();
    }

    private void LoadLastLevel()
    {
        AudioManager.Instance.StopAllSounds();
        
        StartCoroutine(LastLevelTranstion());
        _interactMessenger.OnInteractInput -= LoadLastLevel;
    }

    IEnumerator LastLevelTranstion()
    {
        CamerasManager.SwitchActiveCamera(CamerasManager.MainCamera);
        
        CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Cutscene");
        
        float elapsedTime = 0;
        _initialFocalLength = CamerasManager.GetFocalLength();
        
        while (elapsedTime < _focalLengthLerpDuration)
        {
            float t =  elapsedTime / _focalLengthLerpDuration;
            CamerasManager.SetFocalLength(Mathf.Lerp(_initialFocalLength, _targetFocalLength, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        CamerasManager.SetFocalLength(_targetFocalLength);
        
        SceneManager.UnloadSceneAsync(_prideLevel);
        SceneManager.LoadSceneAsync(_alienLabLevel, LoadSceneMode.Additive);
        
        elapsedTime = 0;
        while (elapsedTime <  _focalLengthLerpDuration)
        {
            float t =  elapsedTime / _focalLengthLerpDuration;
            CamerasManager.SetFocalLength(Mathf.Lerp(_targetFocalLength, _initialFocalLength, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        CamerasManager.SetFocalLength(_initialFocalLength);
        CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Player");
    }
}






