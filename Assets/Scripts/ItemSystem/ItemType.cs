using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemType",menuName = "Item/Type")]
public class ItemType : ScriptableObject
{
    public string type;
    public Texture2D icon;
    public float value;
}
