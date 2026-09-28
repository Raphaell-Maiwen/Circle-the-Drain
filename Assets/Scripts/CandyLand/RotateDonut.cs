using UnityEngine;

public class RotateDonut : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }

    public void Accelerate(float acceleration)
    {
        _rotationSpeed *= acceleration;
    }
}
