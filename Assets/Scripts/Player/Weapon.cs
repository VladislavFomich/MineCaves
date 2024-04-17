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

    private float range = 1f;
    private WeaponManager.Weapons _currentWeapon;


    public void Init(int hp, int damage, WeaponManager.Weapons weapon)
    {
        if (weapon == WeaponManager.Weapons.None) { _currentWeapon = WeaponManager.Weapons.None; return; }
        _currentWeapon = weapon;
        _health = hp;
        _damage = damage;
    }


    public void Hit()
    {
        WeaponManager.Instance.ChangeWeaponHP(_currentWeapon, 1);
        _health--;
        hitVfx.Play();
        if (_health == 0)
        {
           DestroyWeapon();
        }
    }


    private void DestroyWeapon()
    {
        _currentWeapon = WeaponManager.Weapons.None;
        destroyWeapon?.Invoke();
        currentModel?.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject.GetComponent<IDamagable>();
        if (target != null)
        {
            Hit();
            //Damage UI
            Vector3 randomSpawnPosition = other.transform.position + new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
            DamageNumber damageNumber = numberPrefab.Spawn(randomSpawnPosition, _damage);
            target.TakeDamage(_damage);
        }

       
    }

    public void ChangeWeapon(GameObject gameObject)
    {
        currentModel = gameObject;
        takeWeapon?.Invoke();
    }
}
