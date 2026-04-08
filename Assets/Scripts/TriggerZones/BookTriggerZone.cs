using System;
using System.Collections;
using UnityEngine;

public class BookTriggerZone : InteractableTriggerZone
{
    [SerializeField] private Transform _bookTransform;
    [SerializeField] private Animator _bookAnimator;
    [SerializeField] private Transform _bookOpenAnchor;
    [SerializeField] private Transform _bookClosedAnchor;
    [SerializeField] private float _openingSpeed;
    [SerializeField] private float _openingRotationSpeed;
    
    [SerializeField] private BookText _bookText;

    private void Start()
    {
        /*_bookAnimator.speed = 0f;
        _bookAnimator.Play("YOUR_ANIMATION_NAME_HERE",0,0);*/
        
        // Fetch the current Animation clip information for the base layer (layer 0)
        AnimatorClipInfo[] m_CurrentClipInfo = _bookAnimator.GetCurrentAnimatorClipInfo(0);

        // Access the Animation clip name (for the first clip in the list)
        if (m_CurrentClipInfo.Length > 0)
        {
            Debug.Log("Current Clip Name: " + m_CurrentClipInfo[0].clip.name);
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






















