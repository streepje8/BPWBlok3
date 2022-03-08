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
    public Mesh mesh;
    public float value;
    public ItemHandler handler;
    public void UseItem(GameObject user)
    {
        handler?.onUse(user);
    }

    internal void Drop(ItemStack itemStack, Vector3 position)
    {
        GameObject dropped = Instantiate(new GameObject(), position, Quaternion.identity);
        DroppedItem droppedItem = (DroppedItem)dropped.AddComponent(typeof(DroppedItem));
        droppedItem.stack = itemStack;
        droppedItem.Drop();
    }
}
