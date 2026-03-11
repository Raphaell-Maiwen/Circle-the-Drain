using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fireworks : MonoBehaviour
{
    [SerializeField] private string _fireworksSong;
    [SerializeField] private string _prideParadeLevel;
    [SerializeField] private float _fireworksShowDuration;
    [SerializeField] private LODGroup _lodGroup;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void InitializeFireworks()
    {
        AudioManager.Instance.PlaySound(_fireworksSong);

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
