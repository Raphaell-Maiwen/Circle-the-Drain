using System;
using Unity.Cinemachine;
using UnityEngine;

public class FirstPersonCharacterController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _gamepadSensitivity;
    [SerializeField] private float _upDownLookRange;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CinemachineCamera _mainCamera;
    [SerializeField] private CinemachineBrain _brain;
    [SerializeField] private CharacterInputHandler _characterInputHandler;

    private Vector3 _currentMovement;
    private float _verticalRotation;

    private void OnEnable()
    {
        CamerasManager.Register(_mainCamera);
        CamerasManager.SetMainCamera(_mainCamera);
    }

    private void OnDisable()
    {
        CamerasManager.Unregister(_mainCamera);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        CamerasManager.SetBrain(_brain);
    }

    private void Update()
    {
        Move();
        HandleRotation();
    }

    public void SetSpeed(float newSpeed)
    {
        _movementSpeed = newSpeed;
    }

    private void Move()
    {
        Vector2 gamepadInput = _characterInputHandler.GamepadMovementInput;

        if (gamepadInput.sqrMagnitude > 0.0001f)
        {
            Vector3 direction = transform.right * gamepadInput.x + transform.forward * gamepadInput.y;
            direction = Vector3.ClampMagnitude(direction, 1f);
            _rigidbody.linearVelocity = direction * _movementSpeed;
        }
        else if (_characterInputHandler.MovementTriggered)
        {
            _rigidbody.linearVelocity = transform.forward * _movementSpeed;
        }
        else
        {
            _rigidbody.linearVelocity = Vector3.zero;
        }
    }

    private void ApplyHorizontalRotation(float rotationAmount)
    {
        transform.Rotate(0, rotationAmount, 0);
    }

    private void ApplyVerticalRotation(float rotationAmount)
    {
        _verticalRotation = Mathf.Clamp(_verticalRotation - rotationAmount, -_upDownLookRange, _upDownLookRange);
        _mainCamera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
    }

    private void HandleRotation()
    {
        Vector2 rotationInput = _characterInputHandler.RotationInput;
        bool isGamepad = _characterInputHandler.IsGamepadRotation;

        float rotationX;
        float rotationY;

        if (isGamepad)
        {
            rotationX = rotationInput.x * _gamepadSensitivity * Time.deltaTime;
            rotationY = rotationInput.y * _gamepadSensitivity * Time.deltaTime;
        }
        else
        {
            rotationX = rotationInput.x * _mouseSensitivity;
            rotationY = rotationInput.y * _mouseSensitivity;
        }

        ApplyHorizontalRotation(rotationX);
        ApplyVerticalRotation(rotationY);
    }
    
    public void SetVerticalRotation(float pitch)
    {
        _verticalRotation = pitch;
        _mainCamera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
    }
    
    public void ResetVerticalRotation()
    {
        _verticalRotation = 0f;
        _mainCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Name: " +  other.gameObject.name);
    }
}
