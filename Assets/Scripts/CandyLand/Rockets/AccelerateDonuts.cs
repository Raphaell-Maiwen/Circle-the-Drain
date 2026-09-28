using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class AccelerateDonuts : Rocket
{
    //Particles too!
    [SerializeField] private AnimationCurve _acceleration;
    [SerializeField] private RotateDonut[] _rotateDonuts;
    
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private float _blendSpeed = 1f;

    public void Accelerate()
    {
        CamerasManager.SwitchActiveCamera(_camera, _blendSpeed);
        CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Cutscene");

        StartCoroutine(ApplyEffect());
    }

    IEnumerator ApplyEffect()
    {
        yield return new WaitForSeconds(_blendSpeed);

        float _timePassed = 0f;

        while (_timePassed < _acceleration.keys[^1].time)
        {
            foreach (var rotateDonut in _rotateDonuts)
            {
                rotateDonut.Accelerate(_acceleration.Evaluate(_timePassed));
            }
            
            _timePassed += Time.deltaTime;
            yield return null;
        }

        foreach (var rotateDonut in _rotateDonuts)
        {
            rotateDonut.Accelerate(_acceleration.Evaluate(_acceleration.keys[^1].time));
        }

        yield return null;
        
        StartCoroutine(StopWatching());
    }
    
    IEnumerator StopWatching()
    {
        CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Player");
        CamerasManager.SwitchActiveCamera(CamerasManager.MainCamera, _blendSpeed);
        
        yield return new WaitForSeconds(_blendSpeed);
        _readyToCollect = true;
    }
}
