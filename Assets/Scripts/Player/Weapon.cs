using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DamageNumbersPro;

public class Weapon : MonoBehaviour
{
    private GameObject currentModel;
    [SerializeField] private DamageNumber numberPrefab;
    [SerializeField] private ParticleSystem hitVfx;
    public UnityAction destroyWeapon;
    public UnityAction takeWeapon;

    private int _health;
    private int _damage;


    public void Init(int Hp, int Damage)
    {
        _health = Hp;
        _damage = Damage;
    }


    public void Hit()
    {
        _health--;
        DamageNumber damageNumber = numberPrefab.Spawn(transform.position, _damage);
        hitVfx.Play();
        if (_health == 0)
        {
           DestroyWeapon();
        }
    }


    private void DestroyWeapon()
    {
        destroyWeapon?.Invoke();
        currentModel?.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        var target = other.gameObject.GetComponent<Resource>();
        if (target != null)
        {
            Hit();
            target.TakeDamage(_damage);
        }

        var enemy = other.gameObject.GetComponent<HumanoidBase>();
        if(enemy != null)
        {
            Hit();
            enemy.TakeDamage(_damage);
        }
    }

    public void ChangeWeapon(GameObject gameObject)
    {
        currentModel = gameObject;
        takeWeapon?.Invoke();
    }
}
