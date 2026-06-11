    using System;
    using UnityEngine;

public class StartTVTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TVStaticManager.Instance.PlayAlienCutscene();
            Destroy(gameObject);
        }
    }
}
