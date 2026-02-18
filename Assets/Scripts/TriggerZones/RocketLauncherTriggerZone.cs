using TMPro;
using UnityEngine;

public class RocketLauncherTriggerZone : InteractableTriggerZone
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private CandyLevelProgress _progress;

    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();

        if (!_progress.IsThresholdReached)
        {
            _text.text = "You need " + (_progress.Threshold - _progress.Count) + " more rockets.";
        }
        else
        {
            _text.text = "Right-click to launch rockets";
        }
    }

    protected override void OnInteractPressed()
    {
        if (_progress.IsThresholdReached)
        {
            Debug.Log("Youpi!");
        }
        else
        {
            Debug.Log("Non.");
        }
    }
}
