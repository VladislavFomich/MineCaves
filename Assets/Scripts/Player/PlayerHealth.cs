using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _hp;
    private PlayerHealthUI _playerHealthUI;

    public void Init(int hp)
    { 
        _hp = hp;
        _playerHealthUI = FindObjectOfType<PlayerHealthUI>();
        if (_playerHealthUI != null)
        {
            _playerHealthUI.Init(hp);
        }
    }


    public void TakeDamage(int damage)
    {
        if (_hp > 0)
        {
            _hp -= damage;
            _playerHealthUI?.RemoveHeart(damage);
            if(_hp <= 0)
            {
                Death();
            }
        }
        else
        {
            Death();
        }
    }


    private void Death()
    {
        GameSceneManager.Instance.StartScene(3);
        Debug.LogWarning("PlayerDead");
    }
}
