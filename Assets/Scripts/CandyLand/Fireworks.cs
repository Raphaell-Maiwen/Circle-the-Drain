using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fireworks : MonoBehaviour
{
    [SerializeField] private string _fireworksSong;
    [SerializeField] private string _prideParadeLevel;
    [SerializeField] private string _candyLandLevel;
    [SerializeField] private float _fireworksShowDuration;
    [SerializeField] private LODGroup _lodGroup;
    [SerializeField] private List<GameObject> _objectsToDeactivate;
    [SerializeField] private List<GameObject> _objectsToActivate;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public IEnumerator InitializeFireworks()
    {
        AudioManager.Instance.PlaySound(_fireworksSong);

        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = Color.black;

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
