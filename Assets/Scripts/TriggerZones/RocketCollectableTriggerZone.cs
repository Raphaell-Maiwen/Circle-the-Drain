using UnityEngine;

public class RocketCollectableTriggerZone : InteractableTriggerZone
{
    [SerializeField] private GameObject _root;
    [SerializeField] private CandyLevelProgress _progress;
    [SerializeField] private InteractableTriggerZoneEventChannel _zoneChannel;

    private void Awake()
    {
        if (_root == null)
        {
            Debug.LogWarning("Root of " + gameObject.name + " is not set.");

            Transform parent = transform;
            while (parent.parent)
            {
                parent = parent.parent;
            }

            _root = parent.gameObject;
        }
    }

    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        _zoneChannel.PlayerEnter();
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();
        _zoneChannel.PlayerExit();
    }

    protected override void OnInteractPressed()
    {
        _progress.Add();

        //Add something on Audio Manager?

        OnPlayerExit();
        Destroy(_root);
    }
}
