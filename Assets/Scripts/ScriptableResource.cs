using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceData", menuName = "Item/Resource")]
public class ScriptableResource : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private int _hp;
    [SerializeField] private int _count;
    [SerializeField] private string _name;
    [SerializeField] private string _disription;
    [SerializeField] private ResourceManager.ResourceType _resourceType;

    public string Id { get => _id; }
    public int Hp { get => _hp; }
    public int Count { get => _count; }
    public string Name { get => _name; }
    public string Discription { get => _disription; }
    public ResourceManager.ResourceType ResType { get => _resourceType; }
}
