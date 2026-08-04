using System;
using System.Collections;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _transforms;

    [SerializeField] private RectTransform _finalMessage;
    [SerializeField] private GameObject _replayMessage;

    //[SerializeField] private float _finalHeight;
    [SerializeField] private float _timeStop;

    private void Start()
    {
        StartCoroutine(AllowReplay());
    }

    private void Update()
    {
        foreach (var t in _transforms)
        {
            t.Translate(Vector3.up * _speed * Time.deltaTime, Space.World);
        }
    }

    IEnumerator AllowReplay()
    {
        yield return new WaitForSeconds(_timeStop);
        
        _replayMessage.SetActive(true);
        enabled = false;
    }
}
