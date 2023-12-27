using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _hp;
    

    public void Init(int hp)
    { 
        _hp = hp;
    }


    public void TakeDamage(int damage)
    {
        if (_hp > 0)
        {
            _hp -= damage;
        }
        else
        {
            Death();
        }
    }


    private void Death()
    {
        Debug.LogWarning("PlayerDead");
    }
}
