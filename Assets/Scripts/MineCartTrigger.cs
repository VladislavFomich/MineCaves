using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MineCartTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] disableObj;
    [SerializeField] private GameObject[] activateObj;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private int cameraIndex = 2;
    [SerializeField] private float waitTime = 2f;


    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<WaitIndicator>();
        if (player != null)
        {
                player.StartWait(waitTime);
                StartCoroutine(WaitMinecart());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<WaitIndicator>();
        if (player != null)
        {
            StopAllCoroutines();
            player.StopWait();
        }
    }

    private IEnumerator WaitMinecart()
    {
        yield return new WaitForSeconds(waitTime);
        foreach (var item in disableObj)
        {
            item?.SetActive(false);
        }
        foreach (var item in activateObj)
        {
            item?.SetActive(true);
        }
        cameraManager?.SetMainCamera(cameraIndex);
    }
}
