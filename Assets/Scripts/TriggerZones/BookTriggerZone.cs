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
        CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Cutscene");

        /*_interactMessenger.OnInteractPressed?.Invoke(_bookText.BookContent);

        if (!_isOpened)
        {
            StartCoroutine(SwitchMapNextFrame("Cutscene"));
            Debug.Log("Open");
            //Start Coroutine of opening book
        }
        else
        {
            StartCoroutine(SwitchMapNextFrame("Player"));
            Debug.Log("Close");
            //Start Coroutine of closing book
        }

        _isOpened = !_isOpened;*/
    }

    private void CloseBook()
    {
        _isOpened = false;
        _interactMessenger.OnInteractPressed?.Invoke(null);
        CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Player");
    }
}
