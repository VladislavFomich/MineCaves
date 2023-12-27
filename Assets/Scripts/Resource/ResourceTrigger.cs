using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTrigger : MonoBehaviour
{
    private Transform _resource;
    public Transform Recource { get => _resource; }
    private bool _onResource = false;
    public bool OnResource { get => _onResource; }



    public void SetOnResource(bool isTrue, Transform resTrans)
    {
        _onResource = isTrue;
        if(isTrue) _resource = resTrans;
        else _resource = null;
    }
}

