using System.Collections;
using UnityEngine;

public class PlayCanadianBoys : MonoBehaviour
{
    [SerializeField] private string _canadianBoysSong;
    [SerializeField] private CandyLevelProgress _candyLevelProgress;

    public void PlayCanadianRemix()
    {
        AudioManager.Instance.StopSound(AudioManager.Instance.GetLevelSong());
        AudioManager.Instance.PlaySound(_canadianBoysSong);

        StartCoroutine(ResumeRegularSong());
    }

    IEnumerator ResumeRegularSong()
    {
        AudioManager audioManager = AudioManager.Instance;
        
        yield return new WaitForSeconds(5f);
        
        while (audioManager.GetAudioSource(_canadianBoysSong).isPlaying)
        {
            yield return null;
        }

        if (!_candyLevelProgress.IsLevelDone)
        {
            audioManager.PlaySound(AudioManager.Instance.GetLevelSong(), true);
        }
    }
}
