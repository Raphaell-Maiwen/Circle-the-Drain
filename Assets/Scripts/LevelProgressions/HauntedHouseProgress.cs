using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "HauntedHouseProgress", menuName = "Scriptable Objects/HauntedHouseProgress")]
public class HauntedHouseProgress : ScriptableObject
{
    [ReadOnly] public List<BookText>  BookTextsRead = new List<BookText>();
    [SerializeField] private List<BookText> _bookTextsToRead = new List<BookText>();

    public int BooksRemaining => _bookTextsToRead.Count - BookTextsRead.Count;
    
    public event Action<BookText> OnBookRead;
    public event Action OnAllBooksRead;
    
    public bool AreAllBooksRead => BookTextsRead.Count == _bookTextsToRead.Count;

    private void OnEnable()
    {
        Reset();
    }

    public void Read(BookText bookText) 
    {
        if (!BookTextsRead.Contains(bookText) && _bookTextsToRead.Contains(bookText))
        {
            BookTextsRead.Add(bookText);
            OnBookRead?.Invoke(bookText);

            if (BookTextsRead.Count == _bookTextsToRead.Count)
            {
                OnAllBooksRead?.Invoke();
            }
        }
    }

    public void Reset()
    {
        BookTextsRead.Clear();
    }
}
