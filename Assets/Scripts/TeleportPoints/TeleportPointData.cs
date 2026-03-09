using UnityEngine;

[CreateAssetMenu(fileName = "TeleportPoint", menuName = "Scriptable Objects/TeleportPoint")]
public class TeleportPointData : ScriptableObject
{
    public Vector3 position;
    public Quaternion rotation;
}