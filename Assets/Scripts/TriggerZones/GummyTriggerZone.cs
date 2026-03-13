using TMPro;
using UnityEngine;

public class GummyTriggerZone : InteractableTriggerZone
{
    [SerializeField] private GameObject _dialogueWindow;
    [SerializeField] private TextMeshProUGUI _dialogue;
    [SerializeField] private GummyText _text;

    private int dialogueIndex = 0;

    protected override void OnPlayerEnter()
    {
        if (_text._dialogue.Count == 0) return;

        base.OnPlayerEnter();
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();
        _dialogueWindow.SetActive(false);
    }

    protected override void OnInteractPressed()
    {
        _dialogue.text = _text._dialogue[dialogueIndex];
        dialogueIndex++;
        if(dialogueIndex == _dialogue.text.Length) dialogueIndex = 0;
        _dialogueWindow.SetActive(true);
    }
}
