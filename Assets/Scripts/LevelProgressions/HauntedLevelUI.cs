using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HauntedLevelUI : ContextualUI
{
    [SerializeField] private HauntedHouseProgress _progress;
    [SerializeField] private ScrollRect _scrollView;
    [SerializeField] private TextMeshProUGUI _scrollViewText;
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private InteractMessenger _interactMessenger;
    [SerializeField] private TextMeshProUGUI _closeBookMsg;
    [SerializeField] private TextMeshProUGUI _continueReadingBooksMessage;

    private void Start()
    {
        OnEnable();

        _interactMessenger.OnInteractPressed.AddListener(OnInteractPressed);
        _progress.OnBookRead += UpdateReadingBooksMessage;
    }

    private new void OnDisable()
    {
        base.OnDisable();

        _interactMessenger.OnInteractPressed.RemoveListener(OnInteractPressed);
        _progress.OnBookRead -= UpdateReadingBooksMessage;
    }

    private void OnInteractPressed(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            _scrollView.gameObject.SetActive(false);
            ShowZoneMessage(_lastChannel);
            _closeBookMsg.gameObject.SetActive(false);
        }
        else
        {
            _scrollViewText.text = content;
            _scrollView.gameObject.SetActive(true);
            EraseAllMessages();
            _scrollView.verticalNormalizedPosition = 1f;
            _closeBookMsg.gameObject.SetActive(true);
            
            //Coroutine?
            EventSystem.current.SetSelectedGameObject(null); // clear first, avoids ignored-reselect bug
            EventSystem.current.SetSelectedGameObject(_scrollbar.gameObject);
        }
    }

    private void UpdateReadingBooksMessage(BookText booktext)
    {
        if (_progress.AreAllBooksRead)
        {
            _continueReadingBooksMessage.text = "";
        }
        else
        {
            _continueReadingBooksMessage.text = "Continue reading books! " + _progress.BooksRemaining + " left to read.";
        }
    }
}
