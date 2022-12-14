using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use(); // item virtual function
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();

    }
}

public enum EquipmentSlot { Weapon, Shield, Head, Chest, Legs, Feet }
public enum EquipmentMeshRegion { Legs, Arms, Body };
