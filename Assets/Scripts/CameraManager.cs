using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] CinemachineVirtualCamera[] cameras;

    private Camera _mainCamera;
    private Color _mainColor;

    private const int CurrentCamera = 1;
    private const int DisableCamera = 0;

    private bool _inCaveBackground;
    private bool _inCaveClipPlane;


    private void Awake()
    {
        _mainCamera = Camera.main;
        _mainColor = _mainCamera.backgroundColor;
    }

    public void SetMainCamera(int idx)
    {
        foreach (var cam in cameras)
        {
            cam.Priority = 0;
        }
        cameras[idx].Priority = 1;
    }


    public void EntranceCaveBackground()
    {
        _inCaveBackground = !_inCaveBackground;
        if (_inCaveBackground) 
        {
            _mainCamera.backgroundColor = Color.black;
        }
        else
        {
            _mainCamera.backgroundColor = _mainColor;
        }
    }


    public void EntranceCaveClipPlane()
    {
        _inCaveClipPlane = !_inCaveClipPlane;
        if (_inCaveClipPlane)
        {
            cameras[0].m_Lens.FarClipPlane = 35;
            cameras[0].m_Lens.NearClipPlane = 10;
        }
        else
        {
            cameras[0].m_Lens.FarClipPlane = 300;
            cameras[0].m_Lens.NearClipPlane = 0.001f;
        }
    }
}
