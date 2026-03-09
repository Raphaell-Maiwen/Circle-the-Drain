using UnityEngine;

public class MovingFlags : MonoBehaviour
{
    [SerializeField] private float _speed;

    void Update()
    {
        transform.position += transform.right * _speed * Time.deltaTime;
    }
}
