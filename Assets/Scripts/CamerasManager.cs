using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public static class CamerasManager
{
    static List<CinemachineCamera> cameras = new List<CinemachineCamera>();

    public static CinemachineCamera ActiveCamera = null;

    public static bool IsActiveCamera(CinemachineCamera camera)
    {
        return camera == ActiveCamera;
    }

    public static void SwitchActiveCamera(CinemachineCamera camera)
    {
        foreach (var cam in cameras)
        {
            cam.Priority = 0;
        }

        camera.Priority = 10;
        ActiveCamera = camera;
    }

    public static void Register(CinemachineCamera camera)
    {
        cameras.Add(camera);
    }

    public static void Unregister(CinemachineCamera camera)
    {
        cameras.Remove(camera);
    }
}
