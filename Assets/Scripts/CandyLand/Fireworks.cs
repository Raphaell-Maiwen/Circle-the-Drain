using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fireworks : MonoBehaviour
{
    [SerializeField] private string _fireworksSong;
    [SerializeField] private string _prideParadeLevel;
    [SerializeField] private float _fireworksShowDuration;
    [SerializeField] private LODGroup _lodGroup;
    [SerializeField] private List<GameObject> _objectsToDeactivate;
    [SerializeField] private List<GameObject> _objectsToActivate;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void InitializeFireworks()
    {
        AudioManager.Instance.PlaySound(_fireworksSong);

        foreach (var obj in _objectsToDeactivate)
        {
            obj.gameObject.SetActive(false);
        }

        foreach (var obj in _objectsToActivate)
        {
            obj.gameObject.SetActive(true);
        }

        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = Color.black;

        StartCoroutine(LoadPrideParade());
    }

    IEnumerator LoadPrideParade()
    {
        yield return new WaitForSeconds(_fireworksShowDuration);

        _lodGroup.enabled = true;
        AudioManager.Instance.StopAllSounds();
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadSceneAsync(_prideParadeLevel, LoadSceneMode.Additive);

        StartCoroutine(EndLevelTransition());
    }

    IEnumerator EndLevelTransition()
    {
        yield return new WaitForSeconds(2f);

        //Switch camera prioti
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
