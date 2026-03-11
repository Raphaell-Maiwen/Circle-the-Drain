using Unity.Cinemachine;
using UnityEngine;

public class FirstPersonCharacterController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _mouseSensitivity;
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
        if (_characterInputHandler.MovementTriggered)
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
        float mouseXRotation = _characterInputHandler.RotationInput.x * _mouseSensitivity;
        float mouseYRotation = _characterInputHandler.RotationInput.y * _mouseSensitivity;

        ApplyHorizontalRotation(mouseXRotation);
        ApplyVerticalRotation(mouseYRotation);
    }
}
