using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ItemStack
{
    public int count;
    public ItemType type;

    public ItemStack(ItemType type, int count)
    {
        this.count = count;
        this.type = type;
    }

    public ItemStack(ItemType type)
    {
        this.type = type;
        count = 1;
    }
}
