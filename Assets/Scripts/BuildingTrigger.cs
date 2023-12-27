using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _shopCanvas;
    [SerializeField] private GameObject _minionCanvas;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Shop"))
        {
            _shopCanvas.SetActive(true);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("MinionBuilding"))
        {
            _minionCanvas.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shop"))
        {
            _shopCanvas.SetActive(false);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("MinionBuilding"))
        {
            _minionCanvas?.SetActive(false);    
        }
    }
    
}
