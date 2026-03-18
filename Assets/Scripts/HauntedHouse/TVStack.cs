using UnityEngine;
using WorkingTelevisions;

public class TVStack : MonoBehaviour
{
    [SerializeField] private televisionCode[] _televisionArray;

    private void Awake()
    {
        foreach (var tv in _televisionArray)
        {
            tv.tvStatic();
        }
    }
}
