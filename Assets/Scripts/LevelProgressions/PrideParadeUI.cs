using TMPro;
using UnityEngine;

public class PrideParadeUI : ContextualUI
{
    [SerializeField] private TextMeshProUGUI _goToNextLevelMessage;
    [SerializeField] private PrideParadeProgress _progress;

    private new void OnEnable()
    {
        base.OnEnable();
        _progress.OnEndReached += ShowNextLevelMessage;
        _progress.OnLevelDone += HideNextLevelMessage;
        
        base._disableExtraUI.AddListener(HideNextLevelMessage);
        base._restoreState.AddListener(RestoreState);
    }

    private new void OnDisable()
    {
        base.OnDisable();
        _progress.OnEndReached -= ShowNextLevelMessage;
        _progress.OnLevelDone -= HideNextLevelMessage;
        
        base._disableExtraUI.RemoveListener(HideNextLevelMessage);
        base._restoreState.RemoveListener(RestoreState);
    }
    
    private void ShowNextLevelMessage()
    {
        base.EraseAllMessages();
        _goToNextLevelMessage.gameObject.SetActive(true);
    }

    private void HideNextLevelMessage()
    {
        _goToNextLevelMessage.gameObject.SetActive(false);
    }

    private void RestoreState()
    {
        _goToNextLevelMessage.gameObject.SetActive(_progress.IsEndReached);
    }
}
