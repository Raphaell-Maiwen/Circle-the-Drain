using System;
using System.Collections;
using UnityEngine;

public class BookTriggerZone : InteractableTriggerZone
{
    [SerializeField] private Transform _bookTransform;
    [SerializeField] private Animator _bookAnimator;
    [SerializeField] private Transform _bookOpenAnchor;
    [SerializeField] private float _openingSpeed;
    [SerializeField] private float _openingRotationSpeed;
    private Transform _bookClosedAnchor;
    
    [SerializeField] private BookText _bookText;

    private void Start()
    {
        _bookClosedAnchor = _bookTransform;
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

        StartCoroutine(AnimatingBookOpening());
    }

    private IEnumerator AnimatingBookOpening()
    {
        Vector3 distance = _bookOpenAnchor.position - _bookTransform.position;
        float angleDiff = Quaternion.Angle(_bookTransform.rotation, _bookOpenAnchor.rotation);

        bool positionDone = false;
        bool rotationDone = false;

        while(!positionDone || !rotationDone) //(distance.magnitude > 0.1f)
        {
            if (!positionDone)
            {
                distance = _bookOpenAnchor.position - _bookTransform.position;
                if (distance.magnitude <= 0.01f)
                {
                    _bookTransform.position = _bookOpenAnchor.position;
                    positionDone = true;
                }
                else
                {
                    _bookTransform.position += distance.normalized * Time.deltaTime * _openingSpeed;
                }
            }
            if (!rotationDone)
            {
                angleDiff = Quaternion.Angle(_bookTransform.rotation, _bookOpenAnchor.rotation);
                if (angleDiff <= 0.1f)
                {
                    _bookTransform.rotation = _bookOpenAnchor.rotation;
                    rotationDone = true;
                }
                else
                {
                    _bookTransform.rotation = Quaternion.Slerp(
                        _bookTransform.rotation,
                        _bookOpenAnchor.rotation,
                        Time.deltaTime * _openingRotationSpeed
                    );
                }
            }

            yield return null;
        }

        _interactMessenger.OnInteractPressed?.Invoke(_bookText.BookContent);
        //_bookAnimator.enabled = true;
        //_bookAnimator.SetTrigger("OpenBook");
    }
    
    private IEnumerator AnimatingBookClosing()
    {
        Vector3 distance = _bookClosedAnchor.position - _bookTransform.position;
        float angleDiff = Quaternion.Angle(_bookTransform.rotation, _bookClosedAnchor.rotation);

        bool positionDone = false;
        bool rotationDone = false;

        while(!positionDone || !rotationDone) //(distance.magnitude > 0.1f)
        {
            if (!positionDone)
            {
                distance = _bookClosedAnchor.position - _bookTransform.position;
                if (distance.magnitude <= 0.01f)
                {
                    _bookTransform.position = _bookClosedAnchor.position;
                    positionDone = true;
                }
                else
                {
                    _bookTransform.position += distance.normalized * Time.deltaTime * _openingSpeed;
                }
            }
            if (!rotationDone)
            {
                angleDiff = Quaternion.Angle(_bookTransform.rotation, _bookClosedAnchor.rotation);
                if (angleDiff <= 0.1f)
                {
                    _bookTransform.rotation = _bookClosedAnchor.rotation;
                    rotationDone = true;
                }
                else
                {
                    _bookTransform.rotation = Quaternion.Slerp(
                        _bookTransform.rotation,
                        _bookClosedAnchor.rotation,
                        Time.deltaTime * _openingRotationSpeed
                    );
                }
            }

            yield return null;
        }
        
        _interactMessenger.OnInteractPressed?.Invoke(_bookText.BookContent);
    }

    private void CloseBook()
    {
        _interactMessenger.OnInteractPressed?.Invoke(null);

        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Player").Enable();
        CharacterInputHandler.Instance.PlayerInput.actions.FindActionMap("Cutscene").Disable();

        CharacterInputHandler.Instance.DisableToggleReadingBook();
        
        StartCoroutine(AnimatingBookClosing());
    }
}






















