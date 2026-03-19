using UnityEngine;

public class BookTriggerZone : InteractableTriggerZone
{
    [SerializeField] private BookText _bookText;
    private bool _isOpened;

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
        _interactMessenger.OnInteractPressed?.Invoke(_bookText.BookContent);

        if (!_isOpened)
        {
            CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Cutscene");
            //Start Coroutine of opening book
        }
        else
        {
            CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Player");
            //Start Coroutine of closing book
        }

        _isOpened = !_isOpened;
    }
}
