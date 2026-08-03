using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : InteractableTriggerZone
{
    [SerializeField] private string _finalCutscene;
    
    protected override void OnInteractPressed(string str)
    {
        _interactMessenger.OnInteractInput -= base.TriggerInteract;
        SceneManager.LoadScene(_finalCutscene);
    }
}
