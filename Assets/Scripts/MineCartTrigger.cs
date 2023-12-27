using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCartTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] disableObj;
    [SerializeField] private GameObject[] activateObj;


    private void OnTriggerEnter(Collider other)
    {
        PlayerHandler playerHandler = other.GetComponent<PlayerHandler>();
        {
            if (playerHandler != null)
            {
                foreach (var item in disableObj)
                {
                    item.SetActive(false);
                }
                foreach (var item in activateObj)
                {
                    item.SetActive(true);
                }
        }  }
    }
}
