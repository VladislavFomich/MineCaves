using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCamera;
    [SerializeField] CinemachineVirtualCamera startCamera;

    private const int CurrentCamera = 1;
    private const int DisableCamera = 0;

    public void SetMainCamera()
    {
        startCamera.Priority = DisableCamera;
        mainCamera.Priority = CurrentCamera;
    }
}
