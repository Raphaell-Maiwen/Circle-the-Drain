using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class HauntedLevelUI : ContextualUI
{
    [SerializeField] private GameObject _scrollView;
    [SerializeField] private TextMeshProUGUI _scrollViewText;
    [SerializeField] private InteractMessenger _interactMessenger;
    [SerializeField] private TextMeshProUGUI _closeBookMsg;

    private new void OnEnable()
    {
        base.OnEnable();

        _interactMessenger.OnInteractPressed.AddListener(OnInteractPressed);
        CharacterInputHandler.Instance.OnCutsceneInteract += CloseBook;

    }

    private new void OnDisable()
    {
        base.OnDisable();

        _interactMessenger.OnInteractPressed.RemoveListener(OnInteractPressed);
        CharacterInputHandler.Instance.OnCutsceneInteract -= CloseBook;
    }

    private void OnInteractPressed(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            Debug.LogError("Book text is empty");
        }
        else
        {
            _scrollViewText.text = content;
            _scrollView.SetActive(!_scrollView.activeSelf);
            EraseAllMessages();
            _closeBookMsg.gameObject.SetActive(true);
        }
    }

    private void CloseBook()
    {
        _scrollView.SetActive(!_scrollView.activeSelf);
        ShowZoneMessage(_lastChannel);

        _closeBookMsg.gameObject.SetActive(false);
    }
}
