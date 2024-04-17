using NHance.Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 7;
    [SerializeField] private int health = 10;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private PlayerMove.PlayerState playerFirstState;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private PlayerHealth palyerHealth;
    [SerializeField] private Weapon weapon;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private WaitIndicator waitIndicator;

    private Coroutine waitCoroutine;

    private void Awake()
    {
        playerMove.Init(moveSpeed, rotationSpeed, playerFirstState);
        palyerHealth.Init(health);
       // ResourceManager.Instance.Init();
       // WeaponManager.Instance.ChangeWeapon(WeaponManager.Weapons.None);
        weapon.destroyWeapon += DestroyWeapon;
        weapon.takeWeapon += TakeWeapon;
    }


    private void DestroyWeapon()
    {
        playerMove.CanCut(false);
    }

    private void TakeWeapon()
    {
        playerMove.CanCut(true);
    }


    private void OnDestroy()
    {
        weapon.destroyWeapon -= DestroyWeapon;
        weapon.takeWeapon -= TakeWeapon;
    }
}
