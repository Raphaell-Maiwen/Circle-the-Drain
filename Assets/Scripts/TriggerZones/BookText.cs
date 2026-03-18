using UnityEngine;

[CreateAssetMenu(fileName = "BookText", menuName = "Scriptable Objects/BookText")]
public class BookText : ScriptableObject
{
    [SerializeField] private string _bookContent;
    public string BookContent => _bookContent;
}
