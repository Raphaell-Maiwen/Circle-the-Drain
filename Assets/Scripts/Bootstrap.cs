using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private string _boostrapScene;
    [SerializeField] private string _startingScene;
    [SerializeField] private string _persitentObjects;

    private void Start()
    {
        SceneManager.LoadSceneAsync(_persitentObjects, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(_startingScene, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(_boostrapScene);
    }
}
