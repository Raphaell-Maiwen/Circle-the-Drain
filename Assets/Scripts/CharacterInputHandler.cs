using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset _playerControls;
    [SerializeField] private string _actionMapName = "Player";

    [SerializeField] private string _movement = "Movement";
    [SerializeField] private string _rotation = "Rotation";
    [SerializeField] private string _interact = "Interact";

    private InputAction _movementAction;
    private InputAction _rotationAction;
    private InputAction _interactAction;

    [SerializeField] private InteractMessenger _interactMessenger;

    public Vector2 RotationInput { get; private set; }
    public bool MovementTriggered { get; private set; }

    public bool InteractTriggered { get; private set; }

    private void Awake()
    {
        InputActionMap mapReference = _playerControls.FindActionMap(_actionMapName);

        _movementAction = mapReference.FindAction(_movement);
        _rotationAction = mapReference.FindAction(_rotation);
        _interactAction = mapReference.FindAction(_interact);

        SubscribeEvents();
    }

    public void SubscribeEvents()
    {
        _rotationAction.performed += inputInfo => RotationInput = inputInfo.ReadValue<Vector2>();
        _rotationAction.canceled += inputInfo => RotationInput = Vector2.zero;

        _movementAction.performed += inputInfo => MovementTriggered = true;
        _movementAction.canceled += inputInfo => MovementTriggered = false;

        _interactAction.performed += inputInfo => InteractTriggered = true;
        _interactAction.canceled += inputInfo => InteractTriggered = false;

        _interactAction.performed += _interactMessenger.SendInteractMessage;
    }

    

    private void OnEnable()
    {
        _playerControls.FindActionMap(_actionMapName).Enable();
    }

    private void OnDisable()
    {
        _playerControls.FindActionMap(_actionMapName).Disable();
    }
}
