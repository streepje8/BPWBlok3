using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Ability
 * Wessel Roelofse
 * 01/04/2022
 * 
 * The basis for programming your own abilities
 */
public abstract class Ability : ScriptableObject
{
    public float duration;
    public abstract void AbilityUpdate();
    public abstract void AbilityUse();
    public abstract void AbilityEnd();

    public abstract void AbilityStart();
}
