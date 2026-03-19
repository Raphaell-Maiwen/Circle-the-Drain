using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Audio.GeneratorInstance;

public class CharacterInputHandler : MonoBehaviour
{
    public static CharacterInputHandler Instance { get; private set; }
    [SerializeField] public PlayerInput PlayerInput;

    [SerializeField] private InputActionAsset _playerControls;
    [SerializeField] private string _actionMapName = "Player";
    [SerializeField] private string _cutSceneMapName = "Cutscene";

    [SerializeField] private string _movement = "Movement";
    [SerializeField] private string _rotation = "Rotation";
    [SerializeField] private string _interact = "Interact";

    private InputAction _movementAction;
    private InputAction _rotationAction;
    private InputAction _interactAction;
    private InputAction _cutsceneInteractAction;

    [SerializeField] private InteractMessenger _interactMessenger;
    public event Action OnCutsceneInteract;

    public Vector2 RotationInput { get; private set; }
    public bool MovementTriggered { get; private set; }

    public bool InteractTriggered { get; private set; }
    public bool CutsceneInteractTriggered { get; private set; }

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InputActionMap mapReference = _playerControls.FindActionMap(_actionMapName);

        _movementAction = mapReference.FindAction(_movement);
        _rotationAction = mapReference.FindAction(_rotation);
        _interactAction = mapReference.FindAction(_interact);

        InputActionMap cutsceneMapReference = _playerControls.FindActionMap(_cutSceneMapName);

        _cutsceneInteractAction = cutsceneMapReference.FindAction(_interact);

        SubscribeEvents();
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void SubscribeEvents()
    {
        _rotationAction.performed += inputInfo => RotationInput = inputInfo.ReadValue<Vector2>();
        _rotationAction.canceled += inputInfo => RotationInput = Vector2.zero;

        _movementAction.performed += inputInfo => MovementTriggered = true;
        _movementAction.canceled += inputInfo => MovementTriggered = false;

        _interactAction.performed += inputInfo => InteractTriggered = true;
        _interactAction.canceled += inputInfo => InteractTriggered = false;

        _cutsceneInteractAction.performed += inputInfo => CutsceneInteractTriggered = true;
        _cutsceneInteractAction.canceled += inputInfo => CutsceneInteractTriggered = false;

        _interactAction.performed += _interactMessenger.SendInteractMessage;
        _cutsceneInteractAction.performed += _ => OnCutsceneInteract?.Invoke();

        _interactAction.started += ctx => Debug.Log("Started");
        _interactAction.performed += ctx => Debug.Log("Pressed");
        _interactAction.canceled += ctx => Debug.Log("Canceled");

        _cutsceneInteractAction.started += ctx => Debug.Log("Cutscene Started");
        _cutsceneInteractAction.performed += ctx => Debug.Log("Cutscene Pressed");
        _cutsceneInteractAction.canceled += ctx => Debug.Log("Cutscene Canceled");
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
