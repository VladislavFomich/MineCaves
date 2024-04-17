using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    [SerializeField] private BoxCollider[] boxCollider;


    public void WeaponActive(int i)
    {
            boxCollider[i].enabled = true;
    }

    public void WeaponDisable(int i)
    {
        var weapon = boxCollider[i].GetComponent<EnemyWeapon>();
        if(weapon != null)
        {
            weapon.DamageApplied = false;
        }
        boxCollider[i].enabled = false;
    }
}
