using System.Collections;
using UnityEngine;

public class BookTriggerZone : InteractableTriggerZone
{
    [SerializeField] private BookText _bookText;
    private bool _isOpened;

    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        CharacterInputHandler.Instance.OnCutsceneInteract += CloseBook;
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();
        CharacterInputHandler.Instance.OnCutsceneInteract -= CloseBook;
    }

    protected override void OnInteractPressed(string str)
    {
        _isOpened = true;
        _interactMessenger.OnInteractPressed?.Invoke(_bookText.BookContent);

        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Player").Disable();
        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Cutscene").Enable();

        Debug.Log("Tu me niaises tu");

        CharacterInputHandler.Instance.EnableToggleReadingBook();
    }

    private void CloseBook()
    {
        Debug.Log("Close book");

        _isOpened = false;
        _interactMessenger.OnInteractPressed?.Invoke(null);

        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Player").Enable();
        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Cutscene").Disable();

        CharacterInputHandler.Instance.DisableToggleReadingBook();
    }
}
