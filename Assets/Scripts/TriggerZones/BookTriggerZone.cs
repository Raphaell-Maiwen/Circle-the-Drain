using UnityEngine;

public class BookTriggerZone : InteractableTriggerZone
{
    [SerializeField] private BookText _bookText;
    [SerializeField] private InteractMessenger _messenger;

    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();
    }

    protected override void OnInteractPressed(string str)
    {
        _messenger.OnInteractPressed?.Invoke(_bookText.BookContent);
    }
}
