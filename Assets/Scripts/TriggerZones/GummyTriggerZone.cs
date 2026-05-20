using System;
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

    private int dialogueIndex = 0;

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
        _interactMessenger.OnInteractPressed?.Invoke(null);

        if (dialogueIndex == 0)
        {
            CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Dialogues");
            CamerasManager.SwitchActiveCamera(_gummyCamera, _blendSpeed);
        }
        
        _dialogue.text = _text._dialogue[dialogueIndex];
        dialogueIndex++;
        if (dialogueIndex == _text._dialogue.Count)
        {
            dialogueIndex = 0;
            CharacterInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("Player");
            CamerasManager.SwitchActiveCamera(CamerasManager.MainCamera, _blendSpeed);
        }
        
        _dialogueWindow.SetActive(true);
    }
}
