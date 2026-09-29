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
        
        if (_zoneChannel.GetMessage == null)
        {
            var channel = _zoneChannel;
            channel.GetMessage = () => channel.IsBusy ? " " : channel.DefaultMessage;
        }
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
        _zoneChannel.SetBusy(true); 
        
        yield return null;

        while (!_rocket.ReadyToCollect)
        {
            yield return null;
        }

        _progress.Add();
        _zoneChannel.SetBusy(false);
        OnPlayerExit();
        
        Destroy(_root);
    }
    
    private void OnDestroy()
    {
        if (_collecting && _zoneChannel.IsBusy) _zoneChannel.SetBusy(false);
    }
}
