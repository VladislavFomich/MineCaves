using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private int _damage = 1;


    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject.GetComponent <PlayerHealth>();
        if (target != null)
        {
            target.TakeDamage(_damage);
        }
    }
}
