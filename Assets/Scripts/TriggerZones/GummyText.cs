using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GummyText", menuName = "Scriptable Objects/GummyText")]
public class GummyText : ScriptableObject
{
    [SerializeField] public List<string> _dialogue = new List<string>();
}
