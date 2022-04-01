using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * TileEntity
 * Wessel Roelofse
 * 01/04/2022
 * 
 * A class that manages entities spawned by tiles
 */
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
        GameController.Instance.UpdateEndTile();
    }

    public virtual void onDeath() { }
}
