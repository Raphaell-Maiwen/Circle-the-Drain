using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class AccelerateDonuts : Rocket
{
    //Particles too!
    [SerializeField] private float _acceleration;
    [SerializeField] private RotateDonut[] _rotateDonuts;
    
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private float _watchTime = 3f;
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
        
        foreach (var rotateDonut in _rotateDonuts)
        {
            rotateDonut.Accelerate(_acceleration);
        }
        
        StartCoroutine(StopWatching());
    }
    
    IEnumerator StopWatching()
    {
        yield return new WaitForSeconds(_watchTime);
        CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Player");
        CamerasManager.SwitchActiveCamera(CamerasManager.MainCamera, _blendSpeed);
        
        yield return new WaitForSeconds(_blendSpeed);
        _readyToCollect = true;
    }
}
