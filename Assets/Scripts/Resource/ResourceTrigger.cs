using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTrigger : MonoBehaviour
{
    private Transform _resource;
    public Transform Recource { get => _resource; }
    private bool _onResource = false;
    public bool OnResource { get => _onResource; }



    public void SetOnResource(bool isTrue, Transform resTrans, ResourceManager.ResourceType resourceType)
    {
        _onResource = isTrue;
        if (isTrue)
        {
            _resource = resTrans;
            if (resourceType == ResourceManager.ResourceType.Tree)
            {

                WeaponManager.Instance.SwitchWeapon(WeaponManager.WeaponType.Axe, out bool canAttack);
                if (!canAttack) { _resource = null; _onResource = false; }
            }
            else
            {
                WeaponManager.Instance.SwitchWeapon(WeaponManager.WeaponType.Pickaxe, out bool canAttack);
                if (!canAttack) { _resource = null; _onResource = false; }
            }
        }
        else
        {
            _onResource = false;
            _resource = null;
        }
    }


    public void SetOnEnemy(bool isTrue, Transform enemyTransform)
    {
        _onResource = isTrue;
        if (isTrue)
        {
            _resource = enemyTransform;
            WeaponManager.Instance.SwitchWeapon(WeaponManager.WeaponType.Sword, out bool canAttack);
            if (!canAttack) { _resource = null; _onResource = false; }

        }
        else
        {
            _onResource = false;
            _resource = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<EnemySkeleton>();
        if(enemy != null) 
        {
            SetOnEnemy(false, null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<EnemySkeleton>();
        if (enemy != null)
        {
            Debug.LogWarning(enemy.name);
            Debug.LogWarning(other);
            SetOnEnemy(true, enemy.transform);
        }
    }

}

