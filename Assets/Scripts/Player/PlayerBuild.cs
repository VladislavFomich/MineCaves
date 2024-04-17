using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(WaitIndicator))]
public class PlayerBuild : MonoBehaviour
{
    [SerializeField] private WaitIndicator waitIndicator;
    [SerializeField] private float waitTime = 2f;
    private Building _building;
    

    private void OnTriggerEnter(Collider other)
    {
         _building = other.GetComponent<Building>();
        if (_building != null)
        {
            waitIndicator.endWaitAction += UpgradeBuild;
            waitIndicator.StartWait(waitTime);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        _building = other.GetComponent<Building>();
        if (_building != null)
        {
            waitIndicator.endWaitAction -= UpgradeBuild;
            waitIndicator.StopWait();
            _building.Interact(false);
            _building = null;
        }
    }

   private void UpgradeBuild()
    {
        waitIndicator.endWaitAction -= UpgradeBuild;
        _building?.Interact(true);
    }
}
