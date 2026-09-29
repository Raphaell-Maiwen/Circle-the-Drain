using System.Collections.Generic;
using UnityEngine;

public class VolumeDefaultValues : ScriptableObject
{
    [SerializeField] private float _bloomIntensity;
    [SerializeField] private float _bloomScatter;
    [SerializeField] private float _chromaticAberrationIntensity;
    
    public float  BloomIntensity => _bloomIntensity;
    public float BloomScatter => _bloomScatter;
    public float  ChromaticAberrationIntensity => _chromaticAberrationIntensity;
    
    private List<Coroutine> _coroutinesToStop =  new List<Coroutine>();
    public List<Coroutine> CoroutinesToStop  => _coroutinesToStop;

    public void AddCoroutine(Coroutine coroutine)
    {
        _coroutinesToStop.Add(coroutine);
    }

    public void RemoveCoroutine(Coroutine coroutine)
    {
        _coroutinesToStop.Remove(coroutine);
    }
}
