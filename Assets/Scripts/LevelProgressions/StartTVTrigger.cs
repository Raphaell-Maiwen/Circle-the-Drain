    using System;
    using UnityEngine;

public class StartTVTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TVStaticManager.Instance.PlayAlienCutscene();
    }
}
