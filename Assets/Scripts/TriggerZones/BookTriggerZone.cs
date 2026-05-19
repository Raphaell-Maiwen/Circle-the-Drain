using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookTriggerZone : InteractableTriggerZone
{
    [SerializeField] private Transform _bookTransform;
    [SerializeField] private Transform _bookOpenAnchor;
    [SerializeField] private Transform _bookClosedAnchor;
    [SerializeField] private float _changingStateSpeed;
    [SerializeField] private GameObject _closedBookGO;
    [SerializeField] private GameObject _openedBookGO;
    
    [SerializeField] private BookText _bookText;

    private float _timeSinceChangeStarted = 0f;
    private Coroutine _changingStateCoroutine;

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
        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Player").Disable();
        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Cutscene").Enable();

        if(_changingStateCoroutine != null) StopCoroutine(_changingStateCoroutine);
        _changingStateCoroutine = StartCoroutine(ChangeBookState(_bookClosedAnchor, _bookOpenAnchor, true));
        
        CharacterInputHandler.Instance.EnableToggleReadingBook();
        
        _interactMessenger.OnInteractPressed?.Invoke(_bookText.BookContent);    
    }

    private void CloseBook()
    {
        _interactMessenger.OnInteractPressed?.Invoke(null);
        
        if(_changingStateCoroutine != null) StopCoroutine(_changingStateCoroutine);
        _changingStateCoroutine = StartCoroutine(ChangeBookState(_bookOpenAnchor, _bookClosedAnchor, false));

        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Player").Enable();
        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Cutscene").Disable();

        CharacterInputHandler.Instance.DisableToggleReadingBook();
    }

    IEnumerator ChangeBookState(Transform startingPos, Transform endingPos, bool isOpening)
    {
        //yield return new WaitForSeconds(_changingStateSpeed);
        yield return null;

        _timeSinceChangeStarted = 0f;
        
        _openedBookGO.SetActive(isOpening);
        _closedBookGO.SetActive(!isOpening);

        _changingStateCoroutine = null;
    }
}






















