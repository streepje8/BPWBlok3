using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemType",menuName = "Item/Type")]
public class ItemType : ScriptableObject
{
    public string type;
    public Texture2D icon;
    public float value;
    public ItemHandler handler;
    public void UseItem(GameObject user)
    {
        handler?.onUse(user);
    }
}
