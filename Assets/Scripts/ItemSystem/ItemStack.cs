using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ItemStack
 * Wessel Roelofse
 * 01/04/2022
 * 
 * This class keeps track of an itemstack on runtime
 */
[Serializable]
public class ItemStack
{
    public int count;
    public int typeID = 0;
    [NonSerialized]public ItemType type;

    public ItemStack(ItemType type, int count)
    {
        this.count = count;
        this.type = type;
        this.typeID = type.ID;
    }

    public ItemStack(ItemType type)
    {
        this.type = type;
        this.typeID = type.ID;
        count = 1;
    }

    public void Drop(Vector3 position)
    {
        type.Drop(this, position);
    }

    public void ResolveType()
    {
        this.type = GameController.Instance.ResolveItemType(typeID);
    }
}
