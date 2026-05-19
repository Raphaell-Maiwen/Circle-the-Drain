using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookTriggerZone : InteractableTriggerZone
{
    [SerializeField] private Transform _bookOpenAnchor;
    [SerializeField] private Transform _bookClosedAnchor;
    [SerializeField] private float _changingStateDuration;
    [SerializeField] private GameObject _closedBookGO;
    [SerializeField] private GameObject _openedBookGO;
    
    [SerializeField] private BookText _bookText;
    
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

        if(_changingStateCoroutine == null)
        {
            _changingStateCoroutine = StartCoroutine(ChangeBookState(_closedBookGO.transform, _bookClosedAnchor, _bookOpenAnchor, true, BookOpened));
        }
    }

    private void CloseBook()
    {
        if (_changingStateCoroutine == null)
        {
            _interactMessenger.OnInteractPressed?.Invoke(null);
            _changingStateCoroutine = StartCoroutine(ChangeBookState(_openedBookGO.transform, _bookOpenAnchor, _bookClosedAnchor, false, BookClosed));
        }
    }

    //TODO: Add rotation
    IEnumerator ChangeBookState(Transform currentBook, Transform startingPos, Transform endingPos, bool isOpening, Action endOfCoroutineAction)
    {
        float elapsed = 0f;
        
        while(elapsed < _changingStateDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _changingStateDuration);
            
            currentBook.position = Vector3.Lerp(startingPos.position, endingPos.position, t);
            Debug.Log(currentBook.position);
            currentBook.rotation = Quaternion.Slerp(startingPos.rotation, endingPos.rotation, t);

            yield return null;
        }
        
        _openedBookGO.transform.position = _bookOpenAnchor.position;
        _closedBookGO.transform.position = _bookClosedAnchor.position;
        _openedBookGO.transform.rotation = _bookOpenAnchor.rotation;
        _closedBookGO.transform.rotation = _bookClosedAnchor.rotation;
        
        _openedBookGO.SetActive(isOpening);
        _closedBookGO.SetActive(!isOpening);

        endOfCoroutineAction?.Invoke();
        _changingStateCoroutine = null;
    }

    private void BookOpened()
    {
        CharacterInputHandler.Instance.EnableToggleReadingBook();
        _interactMessenger.OnInteractPressed?.Invoke(_bookText.BookContent);
    }

    private void BookClosed()
    {
        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Player").Enable();
        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Cutscene").Disable();

        CharacterInputHandler.Instance.DisableToggleReadingBook();
    }
}






















