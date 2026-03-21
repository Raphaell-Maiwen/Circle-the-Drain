using UnityEngine;

[CreateAssetMenu(fileName = "BookText", menuName = "Scriptable Objects/BookText")]
public class BookText : ScriptableObject
{
    [TextArea(5, 50)]
    [SerializeField] private string _bookContent;
    public string BookContent => _bookContent;
}
