using TMPro;
using UnityEngine;

public class CandyLevelUI : ContextualUI
{
    [SerializeField] private CandyLevelProgress _progress;
    [SerializeField] private TextMeshProUGUI _teleportMessage;

    private new void OnEnable()
    {
        base.OnEnable();

        _progress.OnThresholdReached += ShowTeleportMessage;
        _progress.OnLevelDone += DisableTeleportUI;
        base._disableExtraUI.AddListener(DisableTeleportUI);
        base._restoreState.AddListener(RestoreState);
    }

    private new void OnDisable()
    {
        base.OnDisable();

        _progress.OnThresholdReached -= ShowTeleportMessage;
        _progress.OnLevelDone -= DisableTeleportUI;
        base._disableExtraUI.RemoveListener(DisableTeleportUI);
        base._restoreState.RemoveListener(RestoreState);
    }

    private void ShowTeleportMessage()
    {
        base.EraseAllMessages();
        _teleportMessage.gameObject.SetActive(true);
    }

    private void DisableTeleportUI()
    {
        _teleportMessage.gameObject.SetActive(false);
    }

    private void RestoreState()
    {
        _teleportMessage.gameObject.SetActive(_progress.IsThresholdReached);
    }
}
