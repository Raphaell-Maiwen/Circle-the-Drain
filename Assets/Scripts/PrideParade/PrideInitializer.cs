using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrideInitializer : LevelInitialization
{
    [SerializeField] private List<Animator> walkAnimatorList;
    [SerializeField] private List<Animator> waveAnimatorList;
    [SerializeField] private string _transitionSong;
    [SerializeField] private string _nightmareSong;
    [SerializeField] private float _nightmareIncreaseRate;

    private Coroutine _increaseNightmareCoroutine;

    void Start()
    {
        Debug.Log("beginning: " + Time.time);
        base.Start();

        foreach (var animator in walkAnimatorList)
        {
            StartCoroutine(StartWalk(animator));
        }

        foreach (var animator in waveAnimatorList)
        {
            animator.SetTrigger("wave");
        }

        StartCoroutine(StartNightmareTransition());
        Debug.Log("end: " + Time.time);
    }

    IEnumerator StartWalk(Animator animator)
    {
        yield return new WaitForSeconds(Random.Range(0, 1f));
        animator.SetTrigger("walk");
    }

    IEnumerator StartNightmareTransition()
    {
        while (AudioManager.Instance.GetAudioSource(_levelSong).isPlaying)
        {
            yield return null;
        }

        Debug.Log("Now " + Time.time);

        foreach(var animator in walkAnimatorList)
        {
            animator.SetTrigger("walk");
        }

        AudioManager.Instance.PlaySound(_transitionSong, false);
        AudioManager.Instance.AddChorusFilter(5f, 1f);
        AudioManager.Instance.AddAudioDistortionFilter(0.3f);

        StartCoroutine(StartNightmare());
        Debug.Log("Now now" + Time.time);
    }

    IEnumerator StartNightmare()
    {
        while (AudioManager.Instance.GetAudioSource(_transitionSong).isPlaying)
        {
            yield return null;
        }

        AudioManager.Instance.PlaySound(_nightmareSong, true);
        StartCoroutine(IncreaseNightmare());
    }

    IEnumerator IncreaseNightmare()
    {
        bool rateCapReached = AudioManager.Instance.AddChorusFilter(0f, 2f, 100f, 10f);
        bool distortionCapReached = AudioManager.Instance.AddAudioDistortionFilter(0.1f, 0.6f);

        if (rateCapReached && distortionCapReached)
        {
            StopCoroutine(_increaseNightmareCoroutine);
        }

        yield return new WaitForSeconds(_nightmareIncreaseRate);
        _increaseNightmareCoroutine = StartCoroutine(IncreaseNightmare());
    }
}
