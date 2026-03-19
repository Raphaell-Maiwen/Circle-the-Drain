using UnityEngine;

public class RocketCollectableTriggerZone : InteractableTriggerZone
{
    [SerializeField] private GameObject _root;
    [SerializeField] private CandyLevelProgress _progress;

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

    protected override void OnInteractPressed(string str)
    {
        _interactMessenger.OnInteractPressed?.Invoke(null);
        _progress.Add();

        //Add something on Audio Manager?

        OnPlayerExit();
        Destroy(_root);
    }
}
