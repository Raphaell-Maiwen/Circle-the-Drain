using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookTriggerZone : InteractableTriggerZone
{
    [SerializeField] private Transform _bookTransform;
    [SerializeField] private Transform _bookOpenAnchor;
    [SerializeField] private Transform _bookClosedAnchor;
    [SerializeField] private float _openingSpeed;
    [SerializeField] private GameObject _closedBookGO;
    [SerializeField] private GameObject _openedBookGO;
    
    [SerializeField] private BookText _bookText;

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
        //Ici pour ouvrir

        _closedBookGO.SetActive(false);
        _openedBookGO.SetActive(true);
        
        CharacterInputHandler.Instance.EnableToggleReadingBook();
        
        _interactMessenger.OnInteractPressed?.Invoke(_bookText.BookContent);    
    }

    private void CloseBook()
    {
        _interactMessenger.OnInteractPressed?.Invoke(null);
        
        //Ici pour fermer
        _closedBookGO.SetActive(true);
        _openedBookGO.SetActive(false);

        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Player").Enable();
        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Cutscene").Disable();

        CharacterInputHandler.Instance.DisableToggleReadingBook();
    }
}






















