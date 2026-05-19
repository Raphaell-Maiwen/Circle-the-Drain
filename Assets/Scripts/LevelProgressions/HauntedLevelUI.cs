using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class HauntedLevelUI : ContextualUI
{
    [SerializeField] private GameObject _scrollView;
    [SerializeField] private TextMeshProUGUI _scrollViewText;
    [SerializeField] private InteractMessenger _interactMessenger;
    [SerializeField] private TextMeshProUGUI _closeBookMsg;

    private void Start()
    {
        OnEnable();

        _interactMessenger.OnInteractPressed.AddListener(OnInteractPressed);
    }

    private new void OnDisable()
    {
        base.OnDisable();

        _interactMessenger.OnInteractPressed.RemoveListener(OnInteractPressed);
    }

    private void OnInteractPressed(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            Debug.LogError("Book text is empty");
            _scrollView.SetActive(false);
            ShowZoneMessage(_lastChannel);
            _closeBookMsg.gameObject.SetActive(false);
        }
        else
        {
            _scrollViewText.text = content;
            _scrollView.SetActive(true);
            EraseAllMessages();
            _closeBookMsg.gameObject.SetActive(true);
        }
    }
}
