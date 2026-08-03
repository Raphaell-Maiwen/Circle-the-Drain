using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class BookTriggerZone : InteractableTriggerZone
{
    [SerializeField] private HauntedHouseProgress _progress;
    [SerializeField] private Transform _bookOpenAnchor;
    [SerializeField] private Transform _bookClosedAnchor;
    [SerializeField] private float _changingStateDuration;
    [SerializeField] private GameObject _closedBookGO;
    [SerializeField] private GameObject _openedBookGO;
    
    [SerializeField] private BookText _bookText;
    [SerializeField] private CinemachineCamera _bookCamera;
    [SerializeField] private float _blendSpeed;
    
    private Coroutine _changingStateCoroutine;

    private void OnEnable()
    {
        CamerasManager.Register(_bookCamera);
    }

    private void OnDisable()
    {
        CamerasManager.Unregister(_bookCamera);
    }

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
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        CamerasManager.SwitchActiveCamera(_bookCamera, _blendSpeed);
        
        if(_changingStateCoroutine == null)
        {
            _changingStateCoroutine = StartCoroutine(ChangeBookState(_closedBookGO.transform, _bookClosedAnchor, _bookOpenAnchor, true, BookOpened));
        }
    }

    private void CloseBook()
    {
        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Player").Disable();
        
        if (_changingStateCoroutine == null)
        {
            _interactMessenger.OnInteractPressed?.Invoke(null);
            _changingStateCoroutine = StartCoroutine(ChangeBookState(_openedBookGO.transform, _bookOpenAnchor, _bookClosedAnchor, false, BookClosed));
            CamerasManager.SwitchActiveCamera(CamerasManager.MainCamera, _blendSpeed);
        }
    }
    
    IEnumerator ChangeBookState(Transform currentBook, Transform startingPos, Transform endingPos, bool isOpening, Action endOfCoroutineAction)
    {
        float elapsed = 0f;
        
        while(elapsed < _changingStateDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _changingStateDuration);
            
            currentBook.position = Vector3.Lerp(startingPos.position, endingPos.position, t);
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
        _interactMessenger.OnInteractPressed?.Invoke(_bookText.BookContent);
        CharacterInputHandler.Instance.EnableToggleReadingBook();
        
        _progress.Read(_bookText);
    }

    private void BookClosed()
    {
        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Player").Enable();
        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Cutscene").Disable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        CharacterInputHandler.Instance.DisableToggleReadingBook();
    }
}






















