using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileEntity : MonoBehaviour
{
    public float HP = 2f;

    public abstract void SpawnAt(int x, int y);

    public void RegisterSelf()
    {
        GameController.Instance.RegisterEntity(gameObject);
    }

    public void Die()
    {
        onDeath();
        GameController.Instance.DeRegisterEntity(gameObject);
        Destroy(gameObject);
    }

    public virtual void onDeath() { }
}
