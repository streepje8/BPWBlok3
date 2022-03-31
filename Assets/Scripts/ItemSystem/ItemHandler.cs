using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemHandler : ScriptableObject
{
    public abstract void onUse(ItemDisplay i, GameObject user);
}
