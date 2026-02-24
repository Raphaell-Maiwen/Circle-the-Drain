using UnityEngine;

public class MovingFlags : MonoBehaviour
{
    [SerializeField] private float _speed;

    void Update()
    {
        transform.Translate(transform.right * _speed * Time.deltaTime);
    }
}
