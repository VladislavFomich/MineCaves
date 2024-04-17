using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField] private GameObject[] weaponObject = null;
    [SerializeField] private ScriptableWeapon[] weaponScriptable = null;
    [SerializeField] private Weapon weapon;
    [SerializeField] private PlayerWeapon[] currentWeapons;
    [SerializeField] private PlayerInterfaceUi playerInterfaceUI;
    [SerializeField] private ForgeCanvasUI forgeCanvasUI;

    public PlayerWeapon[] CurrentWeapons { get => currentWeapons; }
    private const string saveKey = "weaponSave";

    [Serializable]
    public class PlayerWeapon
    {
        public WeaponType WeaponType;
        public Weapons Weapon;
        public int HP;
    }


    public enum WeaponType
    {
        Pickaxe,
        Axe,
        Sword
    }
    

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
    private void Awake()
    {
        forgeCanvasUI.Init();
        Load();
    }

    public void InitWeapon(Weapons weapon)
    {
        currentWeapon = weapon;
        SetWeapon(weapon);
        foreach (var item in weaponObject) 
        {
            item.SetActive(false);
        }

        if (currentWeapon == Weapons.None)
        {
            this.weapon.Init(0, 0,weapon);
            return;
        }
        else
        {
            this.weapon.Init(GetWeaponInfo(weapon).Hp, GetWeaponInfo(weapon).Damage, weapon);
        }

        weaponObject[(int)currentWeapon - 1].SetActive(true);
        this.weapon.ChangeWeapon(weaponObject[(int)currentWeapon - 1]);
        playerInterfaceUI.InitializeIndicators(weapon);
        forgeCanvasUI.TakeWeapon(weapon, GetWeaponInfo(weapon).Hp);
    }


    public ScriptableWeapon GetWeaponInfo(Weapons weapon)
    {
        return weaponScriptable[(int)weapon - 1];
    }


    private void SetWeapon(Weapons weapon)
    {
        if (currentWeapon == Weapons.None) { return; }
        var type = GetWeaponInfo(weapon);
            switch (type.WeaponType)
            {
                case WeaponType.Pickaxe:
                    currentWeapons[0].Weapon = weapon;
                    currentWeapons[0].HP = type.Hp;
                    break;
                case WeaponType.Axe:
                    currentWeapons[1].Weapon = weapon;
                    currentWeapons[1].HP = type.Hp;
                    break;
                case WeaponType.Sword:
                    currentWeapons[2].Weapon = weapon;
                    currentWeapons[2].HP = type.Hp;
                    break;
            }
    }


    public void DisableWeapon(PlayerWeapon weapon)
    {
          weapon.Weapon = Weapons.None;
    }


    public void ChangeWeaponHP(Weapons weapon, int hp)
    {
        var type = GetWeaponInfo(weapon);
        switch (type.WeaponType)
        {
            case WeaponType.Pickaxe:
                currentWeapons[0].HP -= hp;
                break;
            case WeaponType.Axe:
                currentWeapons[1].HP -= hp;
                break;
            case WeaponType.Sword:
                currentWeapons[2].HP -= hp;
                break;
        }

        foreach (var item in currentWeapons)
        {
            if(item.HP <= 0)
            {
                DisableWeapon(item);
            }
        }
        playerInterfaceUI.UpdateHealthIndicator(weapon, currentWeapons[(int)type.WeaponType].HP);
        
    }


    public void SwitchWeapon(WeaponType weaponType, out bool canAttack)
    {
        currentWeapon = currentWeapons[(int)weaponType].Weapon;
        foreach (var item in weaponObject)
        {
            item.SetActive(false);
        }
        if (currentWeapon == Weapons.None)
        {
            canAttack = false;
            //this.weapon.Init(currentWeapon);
            return;
        }
        else
        {
            this.weapon.Init( currentWeapons[(int)weaponType].HP, GetWeaponInfo(currentWeapon).Damage, currentWeapon);
            weaponObject[(int)currentWeapon - 1].SetActive(true);
            this.weapon.ChangeWeapon(weaponObject[(int)currentWeapon - 1]);
            canAttack = true;
        }
    }


    public void Load()
    {
        Debug.Log("LoadWeapon");
        var data = SaveManager.Load<SaveData.WeaponSaveData>(saveKey);
        InitWeapon((Weapons)data.PickAxeWeapon);     
        InitWeapon((Weapons)data.AxeWeapon);     
        InitWeapon((Weapons)data.SwordWeapon);
        currentWeapons[0].HP = data.PickAxeHp;
        currentWeapons[1].HP = data.AxeHp;
        currentWeapons[2].HP = data.SwordHp;
        foreach (var item in currentWeapons)
        {
            playerInterfaceUI.UpdateHealthIndicator(item.Weapon, item.HP);
            forgeCanvasUI.TakeWeapon(item.Weapon, item.HP);
        }
    }

    public void Save()
    {
        Debug.Log("SaveWeapon");
        SaveManager.Save(saveKey, GetSaveSnapshot());
    }

    private SaveData.WeaponSaveData GetSaveSnapshot()
    {
        var data = new SaveData.WeaponSaveData()
        {
            PickAxeWeapon = (int)currentWeapons[0].Weapon,
            AxeWeapon = (int)currentWeapons[1].Weapon,
            SwordWeapon = (int)currentWeapons[2].Weapon,

            PickAxeHp = currentWeapons[0].HP,
            AxeHp = currentWeapons[1].HP,
            SwordHp = currentWeapons[2].HP,
        };
        return data;
    }

}
