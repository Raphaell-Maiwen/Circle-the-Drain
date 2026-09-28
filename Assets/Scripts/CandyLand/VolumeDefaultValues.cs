using UnityEngine;

public class VolumeDefaultValues : ScriptableObject
{
    [SerializeField] private float _bloomIntensity;
    [SerializeField] private float _bloomScatter;
    [SerializeField] private float _chromaticAberrationIntensity;
    
    public float  BloomIntensity => _bloomIntensity;
    public float BloomScatter => _bloomScatter;
    public float  ChromaticAberrationIntensity => _chromaticAberrationIntensity;
}
