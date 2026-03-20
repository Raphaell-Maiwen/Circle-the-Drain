using System;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportAction : MonoBehaviour
{
    [SerializeField] private CandyLevelProgress _progress;
    [SerializeField] private InteractMessenger _messenger;
    [SerializeField] private InteractableTriggerZoneEventChannel _rocketCollectableZoneChannel;
    [SerializeField] private InteractableTriggerZoneEventChannel _rocketLaunchZoneChannel;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camera;
    [SerializeField] private TeleportPointData[] _teleportPointArray;

    private void OnEnable()
    {
        _progress.OnThresholdReached += EnableTeleportAction;
        _rocketCollectableZoneChannel.OnPlayerEntered += DisableTeleportAction;
        _rocketCollectableZoneChannel.OnPlayerExited += TryRestoreTeleportAction;
        _rocketLaunchZoneChannel.OnPlayerEntered += DisableTeleportAction;
        _rocketLaunchZoneChannel.OnPlayerExited += TryRestoreTeleportAction;

        if (_progress.IsThresholdReached) EnableTeleportAction();
    }

    private void OnDisable()
    {
        _progress.OnThresholdReached -= EnableTeleportAction;
        _rocketCollectableZoneChannel.OnPlayerEntered -= DisableTeleportAction;
        _rocketCollectableZoneChannel.OnPlayerExited -= TryRestoreTeleportAction;
        _rocketLaunchZoneChannel.OnPlayerEntered -= DisableTeleportAction;
        _rocketLaunchZoneChannel.OnPlayerExited -= TryRestoreTeleportAction;

        _messenger.OnInteractInput -= Teleport;
    }

    private void EnableTeleportAction() => _messenger.OnInteractInput += Teleport;
    private void DisableTeleportAction(InteractableTriggerZoneEventChannel channel) => _messenger.OnInteractInput -= Teleport;
    private void TryRestoreTeleportAction()
    {
        _messenger.OnInteractInput -= Teleport;
        if (_progress.IsThresholdReached) EnableTeleportAction();
    }

    private void Teleport()
    {
        Vector3 teleportPointPosition = _teleportPointArray[0].position;
        Vector3 teleportPointRotation = _teleportPointArray[0].rotation.eulerAngles;

        teleportPointPosition.y = _player.position.y;

        _player.position = teleportPointPosition;

        _player.localEulerAngles = new Vector3(0, teleportPointRotation.y, 0);
        _camera.localEulerAngles = new Vector3(teleportPointRotation.x, 0, 0);
    }
}
