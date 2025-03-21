using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable,
    Resource
}

public enum ConsumableType
{
    Hunger,
    Health,
    Speed,
}

[CreateAssetMenu(fileName = "Item",menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")] public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;
    
    [Header("Stacking")]public bool canStack;
    public int maxStack;
    
    [Header("Consumable")]public ConsumableItem[] consumables;
    
    [Header("Equip")]public GameObject equipPrefab;
}

[System.Serializable]
public class ConsumableItem
{
    public ConsumableType type;
    public float value;
}
