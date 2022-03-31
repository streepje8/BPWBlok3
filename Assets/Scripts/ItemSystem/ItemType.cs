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
    public Material material;
    public float value;
    public ItemHandler handler;
    public int ID = 0;

    public void UseItem(ItemDisplay i, GameObject user)
    {
        handler?.onUse(i, user);
    }

    public void UseItem(GameObject user)
    {
        handler?.onUse(null, user);
    }

    internal void Drop(ItemStack itemStack, Vector3 position)
    {
        GameObject dropped = Instantiate(new GameObject(), position, Quaternion.identity);
        DroppedItem droppedItem = (DroppedItem)dropped.AddComponent(typeof(DroppedItem));
        dropped.layer = LayerMask.NameToLayer("Clickable");
        droppedItem.stack = itemStack;
        droppedItem.Drop();
    }
}
