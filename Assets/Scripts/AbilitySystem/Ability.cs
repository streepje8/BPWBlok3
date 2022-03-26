using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public float duration;
    public abstract void AbilityUpdate();
    public abstract void AbilityUse();
    public abstract void AbilityEnd();

    public abstract void AbilityStart();
}
