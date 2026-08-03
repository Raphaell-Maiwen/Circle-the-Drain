using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private InputActionAsset _inputActionAsset;
    [SerializeField] private InteractMessenger _interactMessenger;
    [SerializeField] private string _dialoguesMapName = "Dialogues";
    [SerializeField] private string _interact = "Interact";
    [SerializeField] private string _bootstrapScene;

    private InputAction _cutsceneInteractAction;
    
    public bool CutsceneInteractTriggered { get; private set; }

    private void Awake()
    {
        InputActionMap cutsceneMapReference = _inputActionAsset.FindActionMap(_dialoguesMapName);
        _cutsceneInteractAction = cutsceneMapReference.FindAction(_interact);
        _playerInput.SwitchCurrentActionMap(_dialoguesMapName);
        SubscribeEvents();
    }

    public void SubscribeEvents()
    {
        _cutsceneInteractAction.performed += _interactMessenger.SendInteractMessage;
    }

    private void OnEnable()
    {
        _interactMessenger.OnInteractInput += RestartGame;
    }

    private void OnDisable()
    {
        _interactMessenger.OnInteractInput -= RestartGame;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(_bootstrapScene);
    }
}
