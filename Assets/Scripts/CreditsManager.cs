using System;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _transforms;

    [SerializeField] private RectTransform _finalMessage;
    [SerializeField] private GameObject _replayMessage;

    [SerializeField] private float _finalHeight;

    private void Start()
    {
        
    }

    private void Update()
    {
        foreach (var t in _transforms)
        {
            t.Translate(Vector3.up * _speed * Time.deltaTime, Space.World);
        }
        
        if (_finalMessage.position.y >= _finalHeight)
        {
            _replayMessage.SetActive(true);
            enabled = false;
        }
    }
}
