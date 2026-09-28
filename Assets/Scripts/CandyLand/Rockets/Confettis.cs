using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Confettis : Rocket
{
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private float _watchTime = 3f;
    [SerializeField] private float _blendSpeed = 1f;
    
    [SerializeField] private float _timeBetweenParticles = 0.3f;
    [SerializeField] private ParticleSystem[] _particles;
    
    public void LaunchConfetti()
    {
        Debug.Log("LaunchConfetti");
        CamerasManager.SwitchActiveCamera(_camera, _blendSpeed);
        CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Cutscene");
        _renderer.enabled = false;
        StartCoroutine(ShowConfettis());
    }

    IEnumerator ShowConfettis()
    {
        yield return new WaitForSeconds(_blendSpeed);

        foreach (ParticleSystem particle in _particles)
        {
            particle.Play();
            yield return new WaitForSeconds(_timeBetweenParticles);
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
