using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField] private GameObject[] weaponObject = null;
    [SerializeField] private ScriptableWeapon[] weaponScriptable = null;
    [SerializeField] private Weapon weapon;
    
    private PickableItem[] _pickableItems = null;

    
    public enum Weapons
    {
        None,
        StonePickAxe,
        MetalPickAxe,
        GoldPickAxe,
        GlassPickAxe,
        DiamondPickAxe,

        StoneAxe,
        MetalAxe,
        GoldAxe,
        GlassAxe,
        DiamondAxe,

        SwordOne,
        SwordTwo,
        SwordThree,
        SwordFour,
        SwordFive
    }

    private Weapons currentWeapon = Weapons.None;


    public void ChangeWeapon(Weapons weapons)
    {
        currentWeapon = weapons;

        foreach (var item in weaponObject) 
        {
            item.SetActive(false);
        }

        if (currentWeapon == Weapons.None)
        {
            weapon.Init(0, 0);
            return;
        }
        else
        {
            weapon.Init(GetWeaponInfo(currentWeapon).Hp, GetWeaponInfo(currentWeapon).Damage);
        }

        weaponObject[(int)currentWeapon - 1].SetActive(true);
        weapon.ChangeWeapon(weaponObject[(int)currentWeapon - 1]);
    }


    public ScriptableWeapon GetWeaponInfo(Weapons weapon)
    {
        return weaponScriptable[(int)weapon - 1];
    }
}
