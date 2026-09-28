using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RocketCollectableTriggerZone : InteractableTriggerZone
{
    [SerializeField] private GameObject _root;
    [SerializeField] private CandyLevelProgress _progress;
    [SerializeField] private UnityEvent _onCollectedEvent;
    [SerializeField] private Rocket _rocket;

    private bool _collecting  = false;

    private void Awake()
    {
        if (_root == null)
        {
            Transform parent = transform;
            while (parent.parent)
            {
                parent = parent.parent;
            }

            _root = parent.gameObject;
        }
        
        _zoneChannel.GetMessage = () => _collecting
            ? ""
            : _zoneChannel.DefaultMessage;
    }

    protected override void OnInteractPressed(string str)
    {
        if (_collecting) return;
        
        _interactMessenger.OnInteractPressed?.Invoke(null);

        _onCollectedEvent?.Invoke();
        StartCoroutine(CollectRocketRoutine());
    }

    private IEnumerator CollectRocketRoutine()
    {
        _collecting = true;
        _zoneChannel.UpdateMessage();
        
        yield return null;

        while (!_rocket.ReadyToCollect)
        {
            yield return null;
        }

        _progress.Add();
        OnPlayerExit();
        
        Destroy(_root);
    }
}
