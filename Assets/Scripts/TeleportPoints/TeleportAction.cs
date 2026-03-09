using System;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportAction : MonoBehaviour
{
    [SerializeField] private CandyLevelProgress _progress;
    [SerializeField] private InteractMessenger _messenger;
    [SerializeField] private InteractableTriggerZoneEventChannel _rocketCollectableZoneChannel;
    [SerializeField] private InteractableTriggerZoneEventChannel _rocketLaunchZoneChannel;
    [SerializeField] private GameObject _player;
    [SerializeField] private TeleportPointData[] _teleportPointArray;

    private void OnEnable()
    {
        _progress.OnThresholdReached += EnableTeleportAction;
        _rocketCollectableZoneChannel.OnPlayerEntered += DisableTeleportAction;
        _rocketCollectableZoneChannel.OnPlayerExited += TryRestoreTeleportAction;
        _rocketLaunchZoneChannel.OnPlayerEntered += DisableTeleportAction;
        _rocketLaunchZoneChannel.OnPlayerExited += TryRestoreTeleportAction;
    }

    private void OnDisable()
    {
        _progress.OnThresholdReached += EnableTeleportAction;
        _rocketCollectableZoneChannel.OnPlayerEntered -= DisableTeleportAction;
        _rocketCollectableZoneChannel.OnPlayerExited -= TryRestoreTeleportAction;
        _rocketLaunchZoneChannel.OnPlayerEntered -= DisableTeleportAction;
        _rocketLaunchZoneChannel.OnPlayerExited -= TryRestoreTeleportAction;
    }

    private void EnableTeleportAction()
    {
        _messenger.AddListener(Teleport);
    }

    private void DisableTeleportAction(InteractableTriggerZoneEventChannel channel)
    {
        _messenger.OnInteractPressed.RemoveListener(Teleport);
    }

    private void TryRestoreTeleportAction()
    {
        if (_progress.IsThresholdReached) EnableTeleportAction();
    }

    private void Teleport()
    {
        Vector3 teleportPoint = _teleportPointArray[0].position;
        teleportPoint.y = _player.transform.position.y;

        _player.transform.position = teleportPoint;
        _player.transform.rotation = _teleportPointArray[0].rotation;
    }
}
