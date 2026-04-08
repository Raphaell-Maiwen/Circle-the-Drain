using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookTriggerZone : InteractableTriggerZone
{
    [SerializeField] private Transform _bookTransform;
    [SerializeField] private Animator _bookAnimator;
    [SerializeField] private Transform _bookOpenAnchor;
    [SerializeField] private Transform _bookClosedAnchor;
    [SerializeField] private float _openingSpeed;
    [SerializeField] private float _openingRotationSpeed;
    
    [SerializeField] private float startAnimationTime;
    [SerializeField] private float endAnimationTime;
    
    [SerializeField] private BookText _bookText;
    
    private AnimatorClipInfo _currentClipInfo;
    private float _clipLength;

    private void Start()
    {
        /*_bookAnimator.speed = 0f;
        _bookAnimator.Play("YOUR_ANIMATION_NAME_HERE",0,0);*/
        
        //Armature|ArmatureAction
        
        // Fetch the current Animation clip information for the base layer (layer 0)
        _currentClipInfo = _bookAnimator.GetCurrentAnimatorClipInfo(0)[0];
        _clipLength = _currentClipInfo.clip.length;
        
        _bookAnimator.Play("Armature|ArmatureAction", 0, startAnimationTime);

        // Access the Animation clip name (for the first clip in the list)
    }

    private void Update()
    {
        AnimatorStateInfo animState = _bookAnimator.GetCurrentAnimatorStateInfo(0);

        // normalizedTime goes past 1.0 when the clip ends (e.g. 1.3 = second loop at 30%)
        float normalizedTime = animState.normalizedTime % 1f;
        float timeInSeconds = normalizedTime * _clipLength;

        Debug.Log("Current time in seconds: " + timeInSeconds);

        // Loop back to start point when we reach the end point
        if (normalizedTime >= endAnimationTime)
        {
            _bookAnimator.Play("Armature|ArmatureAction", 0, startAnimationTime);
        }
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

        CharacterInputHandler.Instance.EnableToggleReadingBook();
        
        _interactMessenger.OnInteractPressed?.Invoke(_bookText.BookContent);    
    }

    private void CloseBook()
    {
        _interactMessenger.OnInteractPressed?.Invoke(null);

        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Player").Enable();
        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Cutscene").Disable();

        CharacterInputHandler.Instance.DisableToggleReadingBook();
    }
}






















