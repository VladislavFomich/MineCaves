using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Item/Weapon")]
public class ScriptableWeapon : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _hp;
    [SerializeField] private int _damage;
    [SerializeField] private string _name;
    [SerializeField] private string _disription;

    public string Id { get => _id; }
    public Sprite Icon { get => _icon; }
    public int Hp { get => _hp; }
    public int Damage { get => _damage; }
    public string Name { get => _name; }
    public string Discription { get => _disription; }
}
