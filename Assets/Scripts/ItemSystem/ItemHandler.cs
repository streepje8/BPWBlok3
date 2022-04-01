using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ItemHandler
 * Wessel Roelofse
 * 01/04/2022
 * 
 * Baseclass for itemHandlers
 */
public abstract class ItemHandler : ScriptableObject
{
    public abstract void onUse(ItemDisplay i, GameObject user);
}
