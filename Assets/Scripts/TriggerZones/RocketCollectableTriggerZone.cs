using UnityEngine;

public class RocketCollectableTriggerZone : InteractableTriggerZone
{
    [SerializeField] private GameObject _root;

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

    protected override void OnInteractPressed()
    {
        //Add a rocket in inventory
        AudioManager.Instance.AddFilter(new AudioDistortionFilter(), 0.1f);
        Destroy(_root);
    }
}
