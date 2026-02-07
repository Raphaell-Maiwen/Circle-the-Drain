using UnityEngine;

public class FireworkTriggerZone : InteractableTriggerZone
{
    protected override void OnInteractPressed()
    {
        Debug.Log("Interact was pressed omg");
    }
}
