using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    [SerializeField] private TeleportPointData teleportPointData;

    private void Start()
    {
        teleportPointData.position = transform.position;
        teleportPointData.rotation = transform.rotation;
    }
}
