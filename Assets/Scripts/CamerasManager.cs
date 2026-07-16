using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public static class CamerasManager
{
    static List<CinemachineCamera> cameras = new List<CinemachineCamera>();

    public static CinemachineCamera ActiveCamera = null;

    public static CinemachineBrain CameraBrain = null;

    public static CinemachineCamera MainCamera = null;
    
    public static GameObject MainCameraGO = null;

    public static bool IsActiveCamera(CinemachineCamera camera)
    {
        return camera == ActiveCamera;
    }

    public static void Register(CinemachineCamera camera)
    {
        if(camera != null) cameras.Add(camera);
    }

    public static void Unregister(CinemachineCamera camera)
    {
        if(camera != null) cameras.Remove(camera);
    }

    public static void SetMainCamera(CinemachineCamera camera)
    {
        MainCamera = camera;
        MainCameraGO = camera.gameObject;
    }

    public static void SetBrain(CinemachineBrain brain)
    {
        CameraBrain = brain;
    }
    
    public static void SwitchActiveCamera(CinemachineCamera camera, float blendSpeed = 5f)
    {
        CameraBrain.DefaultBlend.Time = blendSpeed;
        
        foreach (var cam in cameras)
        {
            cam.Priority = 0;
        }

        camera.Priority = 10;
        ActiveCamera = camera;
    }

    public static void SetFocalLength(float focalLength)
    {
        ActiveCamera.Lens.FieldOfView = Camera.FocalLengthToFieldOfView(focalLength, Camera.main.sensorSize.y);
    }

    public static float GetFocalLength()
    {
        return Camera.FieldOfViewToFocalLength(ActiveCamera.Lens.FieldOfView, Camera.main.sensorSize.y);
    }

    public static IEnumerator ResetCamera()
    {
        MainCameraGO.SetActive(false);
        yield return null;
        MainCameraGO.SetActive(true);
    }
}
















