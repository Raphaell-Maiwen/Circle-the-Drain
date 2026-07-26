using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class LaunchHotBalloons : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _balloons;
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private float _watchTime = 3f;
    [SerializeField] private float _blendSpeed = 1f;
    
    [SerializeField] private float minUpwardForce = 8f;
    [SerializeField] private float maxUpwardForce = 14f;
    [SerializeField] private float horizontalSpread = 3f;
    [SerializeField] private float maxTorque = 2f;
    
    public void LaunchBalloons()
    {
        CamerasManager.SwitchActiveCamera(_camera, _blendSpeed);
        CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Cutscene");
        StartCoroutine(ApplyForce());
    }

    IEnumerator ApplyForce()
    {
        yield return new WaitForSeconds(_blendSpeed);
        foreach (var balloon in _balloons)
        {
            Vector3 direction = new Vector3(
                Random.Range(-horizontalSpread, horizontalSpread),
                Random.Range(minUpwardForce, maxUpwardForce),
                Random.Range(-horizontalSpread, horizontalSpread)
            );

            balloon.AddForce(direction, ForceMode.Impulse);
        }

        StartCoroutine(StopWatching());
    }

    IEnumerator StopWatching()
    {
        yield return new WaitForSeconds(_watchTime);
        CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Player");
        CamerasManager.SwitchActiveCamera(CamerasManager.MainCamera, _blendSpeed);
    }
}
