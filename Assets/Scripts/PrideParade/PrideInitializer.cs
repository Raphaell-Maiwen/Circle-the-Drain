using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrideInitializer : LevelInitialization
{
    [SerializeField] private List<Animator> walkAnimatorList;
    [SerializeField] private List<Animator> waveAnimatorList;
    [SerializeField] private float _nightmareStartTime;
    [SerializeField] private float _nightmareIncreaseRate;

    void Start()
    {
        base.Start();

        foreach (var animator in walkAnimatorList)
        {
            StartCoroutine(StartWalk(animator));
        }

        foreach (var animator in waveAnimatorList)
        {
            animator.SetTrigger("wave");
        }

        StartCoroutine(StartNightmare());
    }

    IEnumerator StartWalk(Animator animator)
    {
        yield return new WaitForSeconds(Random.Range(0, 1f));
        animator.SetTrigger("walk");
    }

    IEnumerator StartNightmare()
    {
        yield return new WaitForSeconds (_nightmareStartTime);

        foreach(var animator in walkAnimatorList)
        {
            animator.SetTrigger("walk");
        }

        AudioManager.Instance.AddChorusFilter(5f, 1f);
        AudioManager.Instance.AddAudioDistortionFilter(0.3f);
        StartCoroutine(IncreaseNightmare());
    }

    IEnumerator IncreaseNightmare()
    {
        yield return new WaitForSeconds(_nightmareIncreaseRate);

        AudioManager.Instance.AddChorusFilter(0f, 2f);
        AudioManager.Instance.AddAudioDistortionFilter(0.1f);
        StartCoroutine(IncreaseNightmare());
    }
}
