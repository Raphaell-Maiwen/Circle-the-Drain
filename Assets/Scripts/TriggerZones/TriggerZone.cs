using UnityEngine;

public abstract class TriggerZone : MonoBehaviour
{
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnPlayerEnter();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnPlayerExit();
        }
    }

    protected abstract void OnPlayerEnter();
    protected abstract void OnPlayerExit();
}
