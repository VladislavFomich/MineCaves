using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform diactivateObject;
    [SerializeField] private ParticleSystem puffSmoke;

    private void Awake()
    {
        var diactivateComponent = diactivateObject.GetComponent<IDeactivatable>();
        if (diactivateObject != null)
        {
            diactivateComponent.OnDeactivate += Dissapire;
        }
    }

    public void Dissapire()
    {
        StartCoroutine(DissapireCoroutine());
    }


    private IEnumerator DissapireCoroutine()
    {
       yield return new WaitForSeconds(0.1f);
       gameObject.SetActive(false);
       puffSmoke?.Play();
    }


    private void OnDisable()
    {
        var diactivateComponent = diactivateObject.GetComponent<IDeactivatable>();
        if (diactivateObject != null)
        {
            diactivateComponent.OnDeactivate -= Dissapire;
        }
    }
}
