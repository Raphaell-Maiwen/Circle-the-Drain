using UnityEngine;

public class HotBalloon : MonoBehaviour
{
    private float _maxHeight;

    void Start()
    {
        _maxHeight = Random.Range(-50, 50);
        _maxHeight += 300;
    }

    void Update()
    {
        if (transform.position.y  >= _maxHeight)
        {
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            Destroy(this);
        }
    }
}
