using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fireworks : MonoBehaviour
{
    [SerializeField] private string _fireworksSong;
    [SerializeField] private string _prideParadeLevel;
    [SerializeField] private float _fireworksShowDuration;
    public void InitializeFireworks()
    {
        AudioManager.Instance.PlaySound(_fireworksSong);

        StartCoroutine(LoadPrideParade());
    }

    IEnumerator LoadPrideParade()
    {
        yield return new WaitForSeconds(_fireworksShowDuration);

        AudioManager.Instance.StopAllSounds();
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadSceneAsync(_prideParadeLevel, LoadSceneMode.Additive);
    }
}
