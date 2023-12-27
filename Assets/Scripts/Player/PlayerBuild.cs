using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBuild : MonoBehaviour
{
    private GameObject _building;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Building"))
        {
            _building = other.gameObject;
            _building.GetComponentInParent<Building>().Interact(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Building"))
        {
            if(_building != null)
            {
                _building.GetComponentInParent<Building>().Interact(false);
                _building = null;
            }
        }
    }

   
}
