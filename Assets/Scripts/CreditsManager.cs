using System;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private RectTransform[] _transforms;
    
    //Faire un scriptable avec toutes les infos des credits
    //Populate on Start

    private void Update()
    {
        foreach (var t in _transforms)
        {
            t.anchoredPosition += new Vector2(0, _speed * Time.deltaTime);
        }
    }
}
