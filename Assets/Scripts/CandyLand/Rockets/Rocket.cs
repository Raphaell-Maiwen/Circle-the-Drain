using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] protected bool _readyToCollect;
    public bool ReadyToCollect => _readyToCollect;
}
