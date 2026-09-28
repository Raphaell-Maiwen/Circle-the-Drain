using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class GummyTriggerZone : InteractableTriggerZone
{
    [SerializeField] private GameObject _dialogueWindow;
    [SerializeField] private TextMeshProUGUI _dialogue;
    [SerializeField] private GummyText _text;
    [SerializeField] private CinemachineCamera _gummyCamera;
    [SerializeField] private float _blendSpeed;
    [SerializeField] private InDialogEventChannel _inDialogueChannel;

    private int dialogueIndex = 0;

    private bool _endingDialogue;

    private void OnEnable()
    {
        CamerasManager.Register(_gummyCamera);
    }

    private void OnDisable()
    {
        CamerasManager.Unregister(_gummyCamera);
    }

    protected override void OnPlayerEnter()
    {
        if (_text._dialogue.Count == 0) return;

        base.OnPlayerEnter();
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();
        _dialogueWindow.SetActive(false);
    }

    protected override void OnInteractPressed(string str)
    {
        if (_endingDialogue) return;
        
        _interactMessenger.OnInteractPressed?.Invoke(null);

        if (dialogueIndex == 0)
        {
            CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Dialogues");
            CamerasManager.SwitchActiveCamera(_gummyCamera, _blendSpeed);
            _inDialogueChannel.StartDialog();
        }
        
        _dialogue.text = _text._dialogue[dialogueIndex];
        dialogueIndex++;
        _dialogueWindow.SetActive(true);
        
        if (dialogueIndex == _text._dialogue.Count)
        {
            dialogueIndex = 0;
            StartCoroutine(EndDialogueRoutine());
        }
    }

    private IEnumerator EndDialogueRoutine()
    {
        _endingDialogue = true;
        
        var interact = CharacterInputHandler.Instance.PlayerInput.actions["Interact"];
        yield return null;

        while (interact.IsPressed())
        {
            yield return null;
        }
        
        CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Player");
        CamerasManager.SwitchActiveCamera(CamerasManager.MainCamera, _blendSpeed);
        _inDialogueChannel.EndDialog();

        _endingDialogue = false;
    }
}




















