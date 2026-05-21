using TMPro;
using UnityEngine;

public class CandyLevelUI : ContextualUI
{
    [SerializeField] private CandyLevelProgress _progress;
    [SerializeField] private TextMeshProUGUI _teleportMessage;
    [SerializeField] private TextMeshProUGUI _continueCollectingRocketsMessage;
    [SerializeField] private string _continueDefaultText;
    [SerializeField] private TextMeshProUGUI _continueDialogMessage;
    [SerializeField] private InDialogEventChannel _dialogEventChannel;

    private new void OnEnable()
    {
        base.OnEnable();

        _progress.OnThresholdReached += ShowTeleportMessage;
        _progress.OnLevelDone += DisableTeleportUI;
        _progress.OnRocketCollected += UpdateCollectingRocketMessage;
        
        //WIP
        _dialogEventChannel.OnStartDialog += ShowContinueDialogMessage;
        _dialogEventChannel.OnEndDialog += HideContinueDialogMessage;
        
        base._disableExtraUI.AddListener(DisableTeleportUI);
        base._restoreState.AddListener(RestoreState);
    }

    private new void OnDisable()
    {
        base.OnDisable();

        _progress.OnThresholdReached -= ShowTeleportMessage;
        _progress.OnLevelDone -= DisableTeleportUI;
        _progress.OnRocketCollected -= UpdateCollectingRocketMessage;
        base._disableExtraUI.RemoveListener(DisableTeleportUI);
        base._restoreState.RemoveListener(RestoreState);
    }

    private void ShowContinueDialogMessage()
    {
        base.EraseAllMessages();
        _continueDialogMessage.gameObject.SetActive(true);
    }

    private void HideContinueDialogMessage()
    {
        base.EraseAllMessages();
        ShowZoneMessage(_lastChannel);
        _continueDialogMessage.gameObject.SetActive(false);
    }

    private void ShowTeleportMessage()
    {
        base.EraseAllMessages();
        _teleportMessage.gameObject.SetActive(true);
        _continueCollectingRocketsMessage.gameObject.SetActive(true);
    }

    private void DisableTeleportUI()
    {
        _teleportMessage.gameObject.SetActive(false);
        _continueCollectingRocketsMessage.gameObject.SetActive(false);
    }

    private void UpdateCollectingRocketMessage()
    {
        if (_progress.RocketsRemaining == 0)
        {
            _continueCollectingRocketsMessage.text = "";
        }
        else
        {
            _continueCollectingRocketsMessage.text = _continueDefaultText + _progress.RocketsRemaining
            + " remaining.";
        }
    }


    private void RestoreState()
    {
        _teleportMessage.gameObject.SetActive(_progress.IsThresholdReached);
        _continueCollectingRocketsMessage.gameObject.SetActive(_progress.IsThresholdReached);
    }
}
