using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrideInitializer : LevelInitialization
{
    [SerializeField] private List<Animator> _animatorList;
    [SerializeField] private float _nightmareStartTime;
    [SerializeField] private float _nightmareIncreaseRate;

    void Start()
    {
        base.Start();

        foreach (var animator in _animatorList)
        {
            StartCoroutine(StartWalk(animator));
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

        foreach(var animator in _animatorList)
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
