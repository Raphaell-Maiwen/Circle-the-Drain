using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableTriggerZone : TriggerZone
{
    [SerializeField] private Canvas _interactableCanvas;
    [SerializeField] private InteractMessenger _interactMessenger;

    protected override void OnPlayerEnter()
    {
        _interactableCanvas.gameObject.SetActive(true);
        _interactMessenger.OnInteractPressed.AddListener(OnInteractPressed);
    }

    protected override void OnPlayerExit()
    {
        _interactableCanvas.gameObject.SetActive(false);
        _interactMessenger.OnInteractPressed.RemoveListener(OnInteractPressed);
    }

    protected abstract void OnInteractPressed();

    private void Awake()
    {
        if (_interactableCanvas == null)
        {
            _interactableCanvas = GetComponentInChildren<Canvas>();
        }

        _interactableCanvas.gameObject.SetActive(false);
    }
}
