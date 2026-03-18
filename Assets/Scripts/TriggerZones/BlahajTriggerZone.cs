using UnityEngine;

public class BlahajTriggerZone : InteractableTriggerZone
{
    [SerializeField] private GameObject _originalShark;
    [SerializeField] private GameObject _transShark;
    //Particles??

    protected override void OnInteractPressed(string str)
    {
        GetComponent<BoxCollider>().enabled = false;
        OnPlayerExit();
        _originalShark.SetActive(false);
        _transShark.SetActive(true);
    }
}
